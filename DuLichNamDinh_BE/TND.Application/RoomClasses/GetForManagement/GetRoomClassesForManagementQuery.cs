using MediatR;
using TND.Domain.Enums;
using TND.Domain.Models;

namespace TND.Application.RoomClasses.GetForManagement
{
    public record GetRoomClassesForManagementQuery(
        string? SearchTerm,
        SortOrder? SortOrder,
        string? SortColumn,
        int PageNumber,
        int PageSize
        ) : IRequest<PaginatedList<RoomClassForManagementResponse>>;
}
