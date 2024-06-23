using MediatR;

namespace TND.Application.Rooms.Update
{
    public record UpdateRoomCommand(
        Guid RoomClassId,
        Guid RoomId,
        string Number) : IRequest;
}
