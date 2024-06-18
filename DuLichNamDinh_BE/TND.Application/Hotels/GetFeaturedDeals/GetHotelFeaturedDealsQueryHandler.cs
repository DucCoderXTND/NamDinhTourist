using AutoMapper;
using MediatR;
using TND.Domain.Interfaces.Persistence.Repositories;

namespace TND.Application.Hotels.GetFeaturedDeals
{
    public class GetHotelFeaturedDealsQueryHandler : IRequestHandler<GetHotelFeaturedDealsQuery, IEnumerable<GetHotelFeaturedDealResponse>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;

        public GetHotelFeaturedDealsQueryHandler(IHotelRepository hotelRepository,
            IRoomClassRepository roomClassRepository,
            IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetHotelFeaturedDealResponse>> Handle(GetHotelFeaturedDealsQuery request, 
            CancellationToken cancellationToken)
        {

            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(request.Count);

            var featuredDeals = await _roomClassRepository.GetFeatureDealsInDifferentHotelsAsync(
               request.Count, cancellationToken);

            return _mapper.Map<IEnumerable<GetHotelFeaturedDealResponse>>(featuredDeals);
        }
    }
}
