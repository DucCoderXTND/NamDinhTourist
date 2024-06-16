using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Domain.Interfaces.Auth
{
    public interface IJwtTokenGenerator
    {
        JwtToken Generate(User user);
    }
}
