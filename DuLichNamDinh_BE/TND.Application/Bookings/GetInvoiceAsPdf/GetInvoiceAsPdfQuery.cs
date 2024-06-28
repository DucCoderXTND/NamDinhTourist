using MediatR;

namespace TND.Application.Bookings.GetInvoiceAsPdf
{
    public record GetInvoiceAsPdfQuery(
        Guid GuestId,
        Guid BookingId) : IRequest<byte[]>;
}
