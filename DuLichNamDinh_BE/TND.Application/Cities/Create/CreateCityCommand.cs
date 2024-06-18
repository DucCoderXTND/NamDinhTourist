using MediatR;

namespace TND.Application.Cities.Create
{
    public record CreateCityCommand(string Name,
        string Country,
        string PostOffice) : IRequest<CityResponse>;
}
