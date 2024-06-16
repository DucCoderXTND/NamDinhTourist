using MediatR;
using TND.Application.Owners.Common;

namespace TND.Application.Owners.Create
{
    public record CreateOwnerCommand(
        string FirstName,
        string LastName,
        string Email,
        string PhoneNumber) : IRequest<OwnerResponse>;
}
