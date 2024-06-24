using AutoMapper;
using TND.Application.Reviews.Common;
using TND.Application.Reviews.Create;
using TND.Application.Reviews.Update;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Application.Mapping
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, ReviewResponse>();
            CreateMap<CreateReviewCommand, Review>();
            CreateMap<UpdateReviewCommand, Review>();
            CreateMap<PaginatedList<Review>, PaginatedList<ReviewResponse>>()
                .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

        }
    }
}
