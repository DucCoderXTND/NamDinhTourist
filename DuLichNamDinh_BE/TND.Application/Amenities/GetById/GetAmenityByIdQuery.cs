using MediatR;
using TND.Application.Amenities.Common;

namespace TND.Application.Amenities.GetById
{
    public record GetAmenityByIdQuery(Guid AmenityId) : IRequest<AmenityResponse>;
}
