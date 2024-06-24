using MediatR;

namespace TND.Application.Reviews.Delete
{
    public record DeleteReviewCommand(
          Guid GuestId,
        Guid HotelId,
        Guid ReviewId) : IRequest;
}
