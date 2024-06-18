using MediatR;

namespace TND.Application.Cities.Update
{
    public record UpdateCityCommand(
        Guid CityId, 
        string Name,
        string Country,
        string PostOffice) : IRequest;
}
