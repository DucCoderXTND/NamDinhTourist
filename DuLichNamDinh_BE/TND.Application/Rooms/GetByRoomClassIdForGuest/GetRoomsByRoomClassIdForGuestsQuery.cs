using MediatR;
using TND.Domain.Models;

namespace TND.Application.Rooms.GetByRoomClassIdForGuest
{
    public record GetRoomsByRoomClassIdForGuestsQuery(
        Guid RoomClassId,
        DateOnly CheckInDate,
        DateOnly CheckOutDate,
        int PageNumber,
        int PageSize) : IRequest<PaginatedList<RoomForGuestResponse>>;

}
