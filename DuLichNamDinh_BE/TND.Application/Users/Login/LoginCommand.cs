using MediatR;

namespace TND.Application.Users.Login
{
    public record LoginCommand(string email, string password) : IRequest<LoginResponse>;
    
}
