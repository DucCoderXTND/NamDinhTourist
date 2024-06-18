using MediatR;
using TND.Domain.Enums;
using TND.Domain.Models;

namespace TND.Application.Cities.GetForManagement
{
    public record GetCitiesForManagementQuery(
        string? SearchTerm,
        SortOrder? SortOrder,
        string? SortColumn,
        int PageNumber,
        int PageSize) : IRequest<PaginatedList<CityForManagementResponse>>;
}
