﻿using AutoMapper;
using TND.Application.Hotels.GetFeaturedDeals;
using TND.Application.RoomClasses.Create;
using TND.Application.RoomClasses.GetByHotelIdForGuest;
using TND.Application.RoomClasses.GetForManagement;
using TND.Application.RoomClasses.Update;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class RoomClassProfile : Profile
    {
        public RoomClassProfile()
        {
            CreateMap<CreateRoomClassCommand, RoomClass>();
            CreateMap<UpdateRoomClassCommand, RoomClass>();

            CreateMap<PaginatedList<RoomClass>, PaginatedList<RoomClassForManagementResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

            CreateMap<PaginatedList<RoomClass>, PaginatedList<RoomClassForGuestResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

            CreateMap<RoomClass, RoomClassForGuestResponse>()
                .ForMember(dst => dst.Amenities, options => options.MapFrom(src => src.Amenities))
                .ForMember(dst => dst.ActiveDiscount, options => options.MapFrom(src =>
                    src.Discounts.FirstOrDefault()))
                .ForMember(dst => dst.GalleryUrls, options => options.MapFrom(src =>
                    src.Gallery.Select(x => x.Path)));

            CreateMap<RoomClass, RoomClassForManagementResponse>()
                .ForMember(dst => dst.Amenities, options => options.MapFrom(src => src.Amenities))
                .ForMember(dst => dst.ActiveDiscount, options => options.MapFrom(
                    src => src.Discounts.FirstOrDefault()));

            CreateMap<RoomClass, HotelFeaturedDealResponse>()
                .ForMember(dst => dst.Id, options => options.MapFrom(src => src.Hotel.Id))
                .ForMember(dst => dst.Name, options => options.MapFrom(src => src.Hotel.Name))
                .ForMember(dst => dst.RoomClassName, options => options.MapFrom(src => src.Name))
                .ForMember(dst => dst.StarRating, options => options.MapFrom(src => src.Hotel.StarRating))
                .ForMember(dst => dst.Longitude, options => options.MapFrom(src => src.Hotel.Longitude))
                .ForMember(dst => dst.Latitude, options => options.MapFrom(src => src.Hotel.Latitude))
                .ForMember(dst => dst.CityName, options => options.MapFrom(src => src.Hotel.City.Name))
                .ForMember(dst => dst.CountryName, options => options.MapFrom(src => src.Hotel.City.Country))
                .ForMember(dst => dst.OriginalPricePerNight, options => options.MapFrom(src => src.PricePerNight))
                .ForMember(dst => dst.DiscountPercentage, options => options.MapFrom(src => src.Discounts.First().Percentage))
                .ForMember(dst => dst.DiscountStartDateUtc, options => options.MapFrom(src => src.Discounts.First().StartDateUtc))
                .ForMember(dst => dst.DiscountEndDateUtc, options => options.MapFrom(src => src.Discounts.First().EndDateUtc))
                .ForMember(dst => dst.ThumbnailUrl, options => options.MapFrom(src => src.Hotel.Thumbnail == null ? null : src.Hotel.Thumbnail.Path));
        }
    }
}
