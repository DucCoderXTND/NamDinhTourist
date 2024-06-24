using MediatR;

namespace TND.Application.Discounts.Delete
{
    public record DeleteDiscountCommand(
        Guid RoomClassId,
        Guid DiscountId) : IRequest;
}
