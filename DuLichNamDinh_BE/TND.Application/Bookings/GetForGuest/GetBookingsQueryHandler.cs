using AutoMapper;
using MediatR;
using TND.Application.Bookings.Common;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;
using TND.Domain.Models;

namespace TND.Application.Bookings.GetForGuest
{
    public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, PaginatedList<BookingResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public GetBookingsQueryHandler(IUserRepository userRepository, IBookingRepository bookingRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<BookingResponse>> Handle(GetBookingsQuery request, 
            CancellationToken cancellationToken = default)
        {
            if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
            {
                throw new NotFoundException(UserMessages.NotFound);
            }

            var query = new PaginationQuery<Booking>(
                b => b.GuestId == request.GuestId,
                Domain.Enums.SortOrder.Ascending,
                null,
                request.PageNumber,
                request.PageSize);

            var bookings = await _bookingRepository.GetAsync(query, cancellationToken);

            return _mapper.Map<PaginatedList<BookingResponse>>(bookings);  
        }
    }
}
