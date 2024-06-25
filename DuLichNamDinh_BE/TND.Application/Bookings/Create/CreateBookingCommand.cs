using MediatR;
using TND.Application.Bookings.Common;
using TND.Domain.Enums;

namespace TND.Application.Bookings.Create
{
    public record CreateBookingCommand(
        Guid GuestId,
        IEnumerable<Guid> RoomIds,
        Guid HotelId,
        DateOnly CheckInDateUtc,
        DateOnly CheckOutDateUtc,
        string? GuestRemarks,
        PaymentMethod PaymentMethod) : IRequest<BookingResponse>;
}
