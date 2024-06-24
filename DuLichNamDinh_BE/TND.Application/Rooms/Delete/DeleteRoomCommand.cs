using MediatR;

namespace TND.Application.Rooms.Delete
{
    public record DeleteRoomCommand(
        Guid RoomClassId, 
        Guid RoomId) : IRequest;
}
