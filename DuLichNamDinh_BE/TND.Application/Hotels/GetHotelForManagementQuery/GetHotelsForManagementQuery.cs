using MediatR;
using TND.Domain.Enums;
using TND.Domain.Models;

namespace TND.Application.Hotels.GetHotelForManagementQuery
{
    public record GetHotelsForManagementQuery(
        string? SearchTerm,
        SortOrder? SortOrder,
        string? SortColumn,
        int PageNumber,
        int PageSize) : IRequest<PaginatedList<HotelForManagementResponse>>;
}
