using MediatR;
using TND.Application.Reviews.Common;

namespace TND.Application.Reviews.Create
{
    public record CreateReviewCommand(
        Guid GuestId,
        Guid HotelId,
        string Content,
        int Rating) : IRequest<ReviewResponse>;
}
