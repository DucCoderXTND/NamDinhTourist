using MediatR;
using TND.Application.Reviews.Common;
using TND.Domain.Enums;
using TND.Domain.Models;

namespace TND.Application.Reviews.GetByHotelId
{
     public record GetReviewsByHotelIdQuery(
         Guid HotelId,
         SortOrder? SortOrder,
         string? SortColumn,
         int PageNumber,
         int PageSize) : IRequest<PaginatedList<ReviewResponse>>;

}
