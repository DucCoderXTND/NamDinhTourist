using AutoMapper;
using TND.Application.Amenities.Common;
using TND.Application.Amenities.Create;
using TND.Application.Amenities.Update;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class AmenityProfile : Profile
    {
        public AmenityProfile()
        {
            CreateMap<PaginatedList<Amenity>, PaginatedList<AmenityResponse>>()
                .ForMember(dst => dst.Items, opt => opt.MapFrom(src => src.Items));
            CreateMap<Amenity, AmenityResponse>();
            CreateMap<UpdateAmenityCommand, Amenity>();
            CreateMap<CreateAmenityCommand, Amenity>();
        }
    }
}
