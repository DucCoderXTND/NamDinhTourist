using MediatR;

namespace TND.Application.Hotels.GetRecentlyVisited
{
    public record GetRecentlyVisitedHotelsForGuestQuery(Guid GuestId, int Count) 
        : IRequest<IEnumerable<RecentlyVisitedHotelResponse>>;
}
