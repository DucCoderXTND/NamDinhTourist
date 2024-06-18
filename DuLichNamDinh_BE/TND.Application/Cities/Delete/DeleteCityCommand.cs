using MediatR;

namespace TND.Application.Cities.Delete
{
    public record DeleteCityCommand(Guid CityId) : IRequest;
}
