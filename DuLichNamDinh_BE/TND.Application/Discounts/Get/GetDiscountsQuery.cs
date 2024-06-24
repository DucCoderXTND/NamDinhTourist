using MediatR;
using TND.Application.RoomClasses.GetById;
using TND.Domain.Enums;
using TND.Domain.Models;

namespace TND.Application.Discounts.Get
{
    public record GetDiscountsQuery(
        Guid RoomClassId,
        SortOrder? sortOder,
        string? SortColumn,
        int PageNumber,
        int PageSize
        ) : IRequest<PaginatedList<DiscountResponse>>;

}
