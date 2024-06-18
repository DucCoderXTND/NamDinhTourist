using MediatR;

namespace TND.Application.Hotels.GetFeaturedDeals
{
    public record GetHotelFeaturedDealsQuery(int Count) : IRequest<IEnumerable<GetHotelFeaturedDealResponse>>;
}
