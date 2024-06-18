using MediatR;

namespace TND.Application.Cities.GetTrending
{
    public record GetTrendingCitiesQuery(int Count) : IRequest<IEnumerable<TrendingCityResponse>>;
}
