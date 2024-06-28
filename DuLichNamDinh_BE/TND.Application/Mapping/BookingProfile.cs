using AutoMapper;
using TND.Application.Bookings.Common;
using TND.Application.Hotels.GetRecentlyVisited;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<PaginatedList<Booking>, PaginatedList<BookingResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

            CreateMap<Booking, BookingResponse>()
                .ForMember(dst => dst.HotelName, options => options.MapFrom(src => src.Hotel.Name));

            CreateMap<Booking, RecentlyVisitedHotelResponse>()
                .ForMember(dst => dst.Id, options => options.MapFrom(src => src.Hotel.Id))
                .ForMember(dst => dst.BookingId, options => options.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, options => options.MapFrom(src => src.Hotel.Name))
                .ForMember(dst => dst.CityName, options => options.MapFrom(src => src.Hotel.City.Name))
                .ForMember(dst => dst.CityCountry, options => options.MapFrom(src => src.Hotel.City.Country))
                .ForMember(dst => dst.StarRating, options => options.MapFrom(src => src.Hotel.StarRating))
                .ForMember(dst => dst.StarRating, options => options.MapFrom(src => src.Hotel.StarRating))
                .ForMember(dst => dst.ReviewsRating, options => options.MapFrom(src => src.Hotel.ReviewsRating))
                .ForMember(dst => dst.StarRating, options => options.MapFrom(src => src.Hotel.StarRating))
                .ForMember(dst => dst.ReviewsRating, options => options.MapFrom(src => src.Hotel.ReviewsRating))
                .ForMember(dst => dst.ThumbnailUrl, options => options
                    .MapFrom(src => src.Hotel.Thumbnail == null ? null : src.Hotel.Thumbnail.Path));



        }
    }
}
