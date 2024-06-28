namespace TND.Application.Bookings.Common
{
    public record BookingResponse(
        Guid Id,
        string HotelName,
        decimal TotalPrice,
        DateOnly CheckInDateUtc,
        DateOnly CheckOutDateUtc,
        DateOnly BookingDateUtc
        );
}
