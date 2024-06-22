using MediatR;

namespace TND.Application.Hotels.Delete
{
    public record DeleteHotelCommand(Guid HotelId) : IRequest;
}
