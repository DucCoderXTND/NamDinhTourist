using MediatR;

namespace TND.Application.Hotels.GetForGuestById
{
    public record GetHotelForGuestByIdQuery(Guid HotelId) : IRequest<HotelForGuestResponse>;
}
