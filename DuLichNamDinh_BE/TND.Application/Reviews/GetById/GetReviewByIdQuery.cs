using MediatR;
using TND.Application.Reviews.Common;

namespace TND.Application.Reviews.GetById
{
    public record GetReviewByIdQuery(
        Guid HotelId,
        Guid ReviewId) : IRequest<ReviewResponse>;
}
