using MediatR;

namespace TND.Application.Bookings.Delete
{
    public record DeleteBookingCommand(
        Guid GuestId,
        Guid BookingId) : IRequest;
}
