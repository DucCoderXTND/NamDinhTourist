using MediatR;
using TND.Application.Owners.Common;
using TND.Domain.Enums;
using TND.Domain.Models;

namespace TND.Application.Owners.Get
{
    public record GetOwnersQuery(
        string? SearchTerm,
        SortOrder? SortOrder,
        string? SortColumn,
        int PageNumber, 
        int PageSize) : IRequest<PaginatedList<OwnerResponse>>;
    
}
