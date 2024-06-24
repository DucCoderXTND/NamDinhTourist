using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TND.Domain.Entities;
using TND.Domain.Exceptions;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Messages;
using TND.Domain.Models;

namespace TND.Application.Rooms.GetForManagement
{
    public class GetRoomsForManagementQueryHandler : IRequestHandler<GetRoomsForManagementQuery, PaginatedList<RoomForManagementResponse>>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IMapper _mapper;

        public GetRoomsForManagementQueryHandler(IRoomClassRepository roomClassRepository, 
            IRoomRepository roomRepository, IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _roomRepository = roomRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RoomForManagementResponse>> Handle(GetRoomsForManagementQuery request, CancellationToken cancellationToken)
        {
            if(await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
            {
                throw new NotFoundException(RoomClassMessages.NotFound);
            }

            var query = new PaginationQuery<Room>(
                GetSearchExpression(request.SearchTerm),
                request.SortOrder ?? Domain.Enums.SortOrder.Ascending,
                request.SortColumn,
                request.PageNumber,
                request.PageSize);

            var owners = await _roomRepository.GetForManagementAsync(query, cancellationToken);

            return _mapper.Map<PaginatedList<RoomForManagementResponse>>(owners);


        }

        private static Expression<Func<Room, bool>> GetSearchExpression(string? searchTerm)
        {
            return searchTerm is null 
                ? _ => true
                : r => r.Number.Contains(searchTerm);
        }
    }
}
