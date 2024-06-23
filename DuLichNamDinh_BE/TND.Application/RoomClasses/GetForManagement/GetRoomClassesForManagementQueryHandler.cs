using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TND.Domain.Entities;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Models;

namespace TND.Application.RoomClasses.GetForManagement
{
    public class GetRoomClassesForManagementQueryHandler : IRequestHandler<GetRoomClassesForManagementQuery,
        PaginatedList<RoomClassForManagementResponse>>
    {
        private readonly IRoomClassRepository _roomClassRepository;
        private readonly IMapper _mapper;

        public GetRoomClassesForManagementQueryHandler(IRoomClassRepository roomClassRepository,
            IMapper mapper)
        {
            _roomClassRepository = roomClassRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<RoomClassForManagementResponse>> Handle(GetRoomClassesForManagementQuery request, CancellationToken cancellationToken)
        {
            var query = new PaginationQuery<RoomClass>(
                GetSearchExpression(request.SearchTerm),
                request.SortOrder ?? Domain.Enums.SortOrder.Ascending,
                request.SortColumn,
                request.PageNumber,
                request.PageSize
                );

            var owners = await _roomClassRepository.GetAsync(
                query, false,
                cancellationToken);

            return _mapper.Map<PaginatedList<RoomClassForManagementResponse>>(owners);
        }

        private static Expression<Func<RoomClass, bool>> GetSearchExpression(string? searchTerm)
        {
            return searchTerm is null
                ? _ => true
                : rc => rc.Name.Contains(searchTerm);
        }
    }
}
