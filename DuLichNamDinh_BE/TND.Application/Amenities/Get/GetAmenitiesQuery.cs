using MediatR;
using TND.Application.Amenities.Common;
using TND.Domain.Enums;
using TND.Domain.Models;

namespace TND.Application.Amenities.Get
{
    public record GetAmenitiesQuery(
        string? SearchTerm,
        SortOrder? SortOrder,
        string? SortColumn,
        int PageNumber,
        int PageSize
        ) : IRequest<PaginatedList<AmenityResponse>>;
}
