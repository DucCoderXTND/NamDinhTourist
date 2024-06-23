using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;
using TND.Domain.Models;

namespace TND.Application.RoomClasses.GetByHotelIdForGuest
{
    public class GetRoomClassesByHotelIdForGuestQueryHandler : IRequestHandler<GetRoomClassesByHotelIdForGuestQuery, 
        PaginatedList<RoomClassForGuestResponse>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;

        public GetRoomClassesByHotelIdForGuestQueryHandler(IHotelRepository hotelRepository,
            IRoomClassRepository roomClassRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RoomClassForGuestResponse>> Handle(GetRoomClassesByHotelIdForGuestQuery request, 
            CancellationToken cancellationToken = default)
        {
            if(!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
            {
                throw new NotFoundException(HotelMessages.NotFound);
            }

            var roomClasses = await _roomClassRepository.GetAsync(
                new PaginationQuery<RoomClass>(
                    rc => rc.HotelId == request.HotelId,
                    Domain.Enums.SortOrder.Ascending,
                    null,
                    request.PageNumber,
                    request.PageSize),
                true,
                cancellationToken);

            return _mapper.Map<PaginatedList<RoomClassForGuestResponse>>(roomClasses);
        }

        
    }
}
