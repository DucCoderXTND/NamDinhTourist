using AutoMapper;
using MediatR;
using TND.Application.Bookings.Common;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Bookings.GetById
{
    public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingResponse>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetBookingByIdQueryHandler(IBookingRepository bookingRepository,
            IUserRepository userRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<BookingResponse> Handle(GetBookingByIdQuery request,
            CancellationToken cancellationToken = default)
        {
            if(!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
            {
                throw new NotFoundException(UserMessages.NotFound);
            }

            var booking = await _bookingRepository
                .GetByIdAsync(request.BookingId, request.GuestId, false, cancellationToken)
                ?? throw new NotFoundException(BookingMessages.NotFoundForGuest);

            return _mapper.Map<BookingResponse>(booking);
        }
    }
}
