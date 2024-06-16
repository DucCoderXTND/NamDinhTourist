using MediatR;
using TND.Domain.Entities;

namespace TND.Application.Amenities.Update
{
    public record UpdateAmenityCommand(Guid AmenityId, string Name, string? Description) : IRequest;

}
