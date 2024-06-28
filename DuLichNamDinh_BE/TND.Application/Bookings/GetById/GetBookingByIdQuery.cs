using MediatR;
using TND.Application.Bookings.Common;

namespace TND.Application.Bookings.GetById
{
    public record GetBookingByIdQuery(
        Guid GuestId,
        Guid BookingId) : IRequest<BookingResponse>;
}
