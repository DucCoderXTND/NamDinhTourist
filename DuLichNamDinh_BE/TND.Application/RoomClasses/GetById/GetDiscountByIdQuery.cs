using MediatR;

namespace TND.Application.RoomClasses.GetById
{
    public record GetDiscountByIdQuery(
        Guid RoomClassId,
        Guid DiscountId) : IRequest<DiscountResponse>;

}
