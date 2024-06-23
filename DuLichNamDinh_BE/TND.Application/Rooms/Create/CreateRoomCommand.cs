using MediatR;

namespace TND.Application.Rooms.Create
{
    public record CreateRoomCommand(
        Guid RoomClassId,
        string Number) : IRequest<Guid>;


}
