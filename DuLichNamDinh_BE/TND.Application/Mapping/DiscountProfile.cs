using AutoMapper;
using TND.Application.Discounts.Create;
using TND.Application.RoomClasses.GetById;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class DiscountProfile : Profile
    {
        public DiscountProfile()
        {
            CreateMap<PaginatedList<Discount>, PaginatedList<DiscountResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
            CreateMap<CreateDiscountCommand, Discount>();
            CreateMap<Discount, DiscountResponse>();
        }
    }
}
