using MediatR;
using TND.Domain.Entities;
using TND.Domain.Enums;

namespace TND.Application.RoomClasses.Create
{
    public record CreateRoomClassCommand(
        Guid HotelId,
        string Name,
        string? Description,
        int AdultsCapacity,
        int ChildrenCapacity,
        decimal PricePerNight,
        IEnumerable<Guid> AmenitiesIds
        ) : IRequest<Guid>;

}
