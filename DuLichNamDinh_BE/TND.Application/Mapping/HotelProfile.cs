using AutoMapper;
using TND.Application.Hotels.Create;
using TND.Application.Hotels.GetForGuestById;
using TND.Application.Hotels.GetHotelForManagementQuery;
using TND.Application.Hotels.Search;
using TND.Application.Hotels.Update;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class HotelProfile : Profile
    {
        public HotelProfile()
        {
            CreateMap<PaginatedList<HotelSearchResult>, PaginatedList<HotelSearchResultResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

            CreateMap<PaginatedList<HotelForManagement>, PaginatedList<HotelForManagementResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

            CreateMap<CreateHotelCommand, Hotel>();
            CreateMap<UpdateHotelCommand, Hotel>();

            CreateMap<Hotel, HotelForGuestResponse>()
                .ForMember(dst => dst.ThumbnailUrl, options =>
                    options.MapFrom(src => src.Thumbnail != null ? src.Thumbnail.Path : null))
                .ForMember(dst => dst.GalleryUrls, options =>
                    options.MapFrom(src => src.Gallery.Select(i => i.Path)));

            CreateMap<HotelForManagement, HotelForManagementResponse>()
                .ForMember(dst => dst.Owner, options => options.MapFrom(src => src.Owner));

            CreateMap<HotelSearchResult, HotelSearchResultResponse>()
                .ForMember(dst => dst.ThumbnailUrl,
                    options => options.MapFrom(src => src.Thumbnail != null ? src.Thumbnail.Path : null));
        }
    }
}
