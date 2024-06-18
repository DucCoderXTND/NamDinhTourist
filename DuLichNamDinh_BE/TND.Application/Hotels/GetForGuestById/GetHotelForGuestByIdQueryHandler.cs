using AutoMapper;
using MediatR;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;

namespace TND.Application.Hotels.GetForGuestById
{
    public class GetHotelForGuestByIdQueryHandler : IRequestHandler<GetHotelForGuestByIdQuery, HotelForGuestResponse>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public GetHotelForGuestByIdQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }
        public async Task<HotelForGuestResponse> Handle(GetHotelForGuestByIdQuery request, CancellationToken cancellationToken)
        {
            var hotelResponse = await _hotelRepository.GetByIdAsync(
                request.HotelId,
                true,true,true,
                cancellationToken) 
                ?? throw new NotFoundException(HotelMessages.NotFound);

            return _mapper.Map<HotelForGuestResponse>(hotelResponse);
        }
    }
}
