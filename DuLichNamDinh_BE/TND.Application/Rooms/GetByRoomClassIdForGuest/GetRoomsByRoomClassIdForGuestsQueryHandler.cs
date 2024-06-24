using AutoMapper;
using MediatR;
using TND.Application.RoomClasses.GetByHotelIdForGuest;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;
using TND.Domain.Models;

namespace TND.Application.Rooms.GetByRoomClassIdForGuest
{
    public class GetRoomsByRoomClassIdForGuestsQueryHandler : IRequestHandler<GetRoomsByRoomClassIdForGuestsQuery, PaginatedList<RoomForGuestResponse>>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GetRoomsByRoomClassIdForGuestsQueryHandler(IRoomClassRepository roomClassRepository,
            IRoomRepository roomRepository, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RoomForGuestResponse>> Handle(GetRoomsByRoomClassIdForGuestsQuery request,
            CancellationToken cancellationToken = default)
        {
            if (!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
            {
                throw new NotFoundException(RoomClassMessages.NotFound);
            }

            var query = new PaginationQuery<Room>(
                r => r.RoomClassId == request.RoomClassId
                &&
                r.Bookings.All(b => b.CheckOutDateUtc <= request.CheckInDate 
                    || b.CheckInDateUtc >= request.CheckOutDate),
                Domain.Enums.SortOrder.Ascending,
                null,
                request.PageNumber,
                request.PageSize);

            var rooms = await _roomRepository.GetAsync(query, cancellationToken);

            return _mapper.Map<PaginatedList<RoomForGuestResponse>>(rooms);
        }
    }
}
