using MediatR;
using TND.Domain.Models;

namespace TND.Application.RoomClasses.GetByHotelIdForGuest
{
    public record GetRoomClassesByHotelIdForGuestQuery(
        Guid HotelId,
        int PageNumber,
        int PageSize) : IRequest<PaginatedList<RoomClassForGuestResponse>>;
}
