using MediatR;
using TND.Application.RoomClasses.GetById;

namespace TND.Application.Discounts.Create
{
    public record CreateDiscountCommand(
        Guid RoomClassId,
        decimal Percentage,
        DateTime StartDateUtc,
        DateTime EndDateUtc) : IRequest<DiscountResponse>;

}
