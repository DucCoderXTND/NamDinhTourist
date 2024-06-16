using MediatR;
using TND.Application.Amenities.Common;

namespace TND.Application.Amenities.Create
{
    public record CreateAmenityCommand(string Name, string? Description) : IRequest<AmenityResponse>;
}
