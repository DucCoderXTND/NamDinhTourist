using MediatR;
using Microsoft.AspNetCore.Http;

namespace TND.Application.Cities.SetThumbnail
{
    public record SetCityThumbnailCommand(Guid CityId, 
        IFormFile Image) : IRequest;
}
