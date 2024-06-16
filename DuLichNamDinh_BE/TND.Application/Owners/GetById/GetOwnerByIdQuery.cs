using MediatR;
using TND.Application.Owners.Common;

namespace TND.Application.Owners.GetById
{
    public record GetOwnerByIdQuery(Guid OwnerId) : IRequest<OwnerResponse>;
    
}
