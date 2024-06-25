using MediatR;
using TND.Application.Bookings.Common;
using TND.Domain.Models;

namespace TND.Application.Bookings.GetForGuest
{
    public record GetBookingsQuery(
        Guid GuestId,
        int PageNumber,
        int PageSize) : IRequest<PaginatedList<BookingResponse>>;
}
