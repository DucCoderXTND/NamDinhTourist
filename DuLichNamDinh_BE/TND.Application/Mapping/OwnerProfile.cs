using AutoMapper;
using TND.Application.Owners.Common;
using TND.Application.Owners.Create;
using TND.Application.Owners.Update;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class OwnerProfile : Profile
    {
        public OwnerProfile()
        {
            CreateMap<CreateOwnerCommand, Owner>();
            CreateMap<UpdateOwnerCommand, Owner>();
            CreateMap<Owner, OwnerResponse>();
            CreateMap<PaginatedList<Owner>, PaginatedList<OwnerResponse>>()
                .ForMember(dst => dst.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
