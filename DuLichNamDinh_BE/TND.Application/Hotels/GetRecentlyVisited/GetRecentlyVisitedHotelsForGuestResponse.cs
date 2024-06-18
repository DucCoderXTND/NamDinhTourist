using AutoMapper;
using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Hotels.GetRecentlyVisited
{
    public class GetRecentlyVisitedHotelsForGuestResponse : IRequestHandler<GetRecentlyVisitedHotelsForGuestQuery, 
        IEnumerable<RecentlyVisitedHotelResponse>>
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetRecentlyVisitedHotelsForGuestResponse(IBookingRepository bookingRepository,
            IHotelRepository hotelRepository, IMapper mapper, IUserRepository userRepository)
        {
            _bookingRepository = bookingRepository;
            _hotelRepository = hotelRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<RecentlyVisitedHotelResponse>> Handle(GetRecentlyVisitedHotelsForGuestQuery request, CancellationToken cancellationToken)
        {
            if (!await _userRepository.ExistsByIdAsync(request.GuestId))
                throw new NotFoundException(UserMessages.NotFound);

            var recentBookingsInDifferentHotels = 
                await _bookingRepository.
                GetRecentBookingsInDifferentHotelsByGuestId(request.GuestId, request.Count, cancellationToken);

            return _mapper.Map<IEnumerable<RecentlyVisitedHotelResponse>>(recentBookingsInDifferentHotels);
        }
    }
}
