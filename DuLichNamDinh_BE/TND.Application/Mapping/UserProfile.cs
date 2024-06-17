using AutoMapper;
using TND.Application.Users.Login;
using TND.Application.Users.Register;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<RegisterCommand, User>();
            CreateMap<JwtToken, LoginResponse>();
        }
    }
}
