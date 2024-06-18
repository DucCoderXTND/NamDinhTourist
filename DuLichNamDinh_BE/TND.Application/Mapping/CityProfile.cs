using AutoMapper;
using TND.Application.Cities.Create;
using TND.Application.Cities.GetForManagement;
using TND.Application.Cities.GetTrending;
using TND.Application.Cities.Update;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<PaginatedList<CityForManagement>, PaginatedList<CityForManagementResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
            CreateMap<CreateCityCommand, City>();
            CreateMap<UpdateCityCommand, City>();
            CreateMap<City, CityResponse>();
            CreateMap<City, TrendingCityResponse>()
                .ForMember(dst => dst.ThumbnailUrl,
                options => options.MapFrom(src => src.Thumbnail != null ? src.Thumbnail.Path : null));
            CreateMap<CityForManagement, CityForManagementResponse>();

        }
    }
}
