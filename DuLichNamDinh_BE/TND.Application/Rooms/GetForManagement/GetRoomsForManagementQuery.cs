using MediatR;
using TND.Domain.Enums;
using TND.Domain.Models;

namespace TND.Application.Rooms.GetForManagement
{
    public record GetRoomsForManagementQuery(
        Guid RoomClassId,
        string? SearchTerm,
        SortOrder? SortOrder,
        string? SortColumn,
        int PageNumber, 
        int PageSize
        ) : IRequest<PaginatedList<RoomForManagementResponse>>;
}
