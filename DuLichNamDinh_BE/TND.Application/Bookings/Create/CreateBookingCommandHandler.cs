using AutoMapper;
using MediatR;
using TND.Application.Bookings.Common;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Interfaces.Services;
using TND.Domain.Messages;

namespace TND.Application.Bookings.Create
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPdfService _pdfService;
        private readonly IEmailService _emailService;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IRoomRepository _roomRepository;

        public CreateBookingCommandHandler(IHotelRepository hotelRepository,
            IUserRepository userRepository,
            IBookingRepository bookingRepository,
            IMapper mapper, IUnitOfWork unitOfWork,
            IPdfService pdfService,
            IEmailService emailService,
            IDateTimeProvider dateTimeProvider,
            IRoomRepository roomRepository
            )
        {
            _hotelRepository = hotelRepository;
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _pdfService = pdfService;
            _emailService = emailService;
            _dateTimeProvider = dateTimeProvider;
            _roomRepository = roomRepository;
        }
        public async Task<BookingResponse> Handle(CreateBookingCommand request,
            CancellationToken cancellationToken = default)
        {
            var guest = await _userRepository
                .GetByIdAsync(request.GuestId, cancellationToken)
                ?? throw new NotFoundException(UserMessages.NotFound);

            var hotel = await _hotelRepository
                .GetByIdAsync(request.HotelId,
                false, false, false,
                cancellationToken)
                ?? throw new NotFoundException(HotelMessages.NotFound);

            var rooms = await ValidateRooms(
                request.RoomIds,
                request.HotelId,
                request.CheckInDateUtc,
                request.CheckOutDateUtc,
                cancellationToken);

            await _unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                var booking = new Booking
                {
                    Hotel = hotel,
                    Rooms = rooms,
                    CheckInDateUtc = request.CheckInDateUtc,
                    CheckOutDateUtc = request.CheckOutDateUtc,
                    TotalPrice = CalculateTotalPrice(rooms,
                        request.CheckInDateUtc, request.CheckOutDateUtc),
                    BookingDateUtc = _dateTimeProvider.GetCurrentDateUtc(),
                    GuestRemarks = request.GuestRemarks,
                    PaymentMethod = request.PaymentMethod
                };

                var createdBooking = await _bookingRepository.CreateAsync(booking, cancellationToken);

                foreach(var room in rooms)
                {
                    var invoiceRecord = new InvoiceRecord
                    {
                        Booking = createdBooking,
                        RoomClassName = room.RoomClass.Name,
                        RoomNumber = room.Number,
                        PriceAtBooking = room.RoomClass.PricePerNight,
                        DiscountPercentageAtBooking = room.RoomClass.Discounts.FirstOrDefault()?.Percentage,

                    };

                    createdBooking.Invoice.Add(invoiceRecord);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var invoicePdf = await _pdfService.GeneratePdfFromHtmlAsync(
                    InvoiceDetailsGenerator.GetInvoiceHtml(createdBooking), cancellationToken);

                await _emailService.SendAsync(
                    BookingEmail.GetBookingEmailRequest(
                        guest.Email,
                        new[] { ("invoice.pdf", invoicePdf) }
                        ), cancellationToken);

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.CommitTransactionAsync(cancellationToken);

                return _mapper.Map<BookingResponse>(createdBooking);
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        }

        private async Task<List<Room>> ValidateRooms(
            IEnumerable<Guid> roomIds,
            Guid hotelId,
            DateOnly CheckInDateUtc,
            DateOnly CheckOutDateUtc,
            CancellationToken cancellationToken = default)
        {
            var rooms = new List<Room>();

            foreach ( var roomId in roomIds )
            {
                var room = await _roomRepository
                    .GetByIdWithRoomClassAsync(roomId, cancellationToken)
                    ?? throw new NotFoundException(RoomMessages.NotFound);

                if(room.RoomClass.HotelId != hotelId)
                {
                    throw new RoomsNotInHotelException(RoomMessages.NotInSameHotel);
                }

                if (!await _roomRepository.IsAvailableAsync(roomId, CheckInDateUtc, CheckOutDateUtc, cancellationToken))
                {
                    throw new RoomNotAvailableException(RoomMessages.NotAvailable(roomId));
                }

                rooms.Add(room);
            }

            return rooms;
        }

        private static decimal CalculateTotalPrice(
            IEnumerable<Room> rooms,
            DateOnly requestCheckInDateUtc,
            DateOnly requestCheckOutDateUtc)
        {
            var totalPricePerNight = rooms.Sum(r => r.RoomClass.PricePerNight
                                        * ((100 - (r.RoomClass.Discounts.FirstOrDefault()?.Percentage ?? 0)) / 100));

            var bookingDuration = requestCheckOutDateUtc.DayNumber - requestCheckInDateUtc.DayNumber;

            return totalPricePerNight * bookingDuration;
        }

    }
}
