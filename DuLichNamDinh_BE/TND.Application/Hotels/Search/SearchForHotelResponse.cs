using AutoMapper;
using MediatR;
using System.Linq.Expressions;
using TND.Application.Extensions;
using TND.Domain.Entities;
using TND.Domain.Interfaces.Persistence.Repositories;
using TND.Domain.Models;

namespace TND.Application.Hotels.Search
{
    public class SearchForHotelResponse : IRequestHandler<SearchForHotelsQuery, PaginatedList<HotelSearchResultResponse>>
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IMapper _mapper;

        public SearchForHotelResponse(IHotelRepository hotelRepository, IMapper mapper)
        {
            _hotelRepository = hotelRepository;
            _mapper = mapper;
        }
        public async Task<PaginatedList<HotelSearchResultResponse>> Handle(SearchForHotelsQuery request, CancellationToken cancellationToken)
        {
            var searchResults = await _hotelRepository.GetForSearchAsync(
                new PaginationQuery<Hotel>(
                    BuildSearchExpression(request),
                    request.SortOrder ?? Domain.Enums.SortOrder.Ascending,
                    request.SortColumn,
                    request.PageNumber,
                    request.PageSize),
                cancellationToken);

            return _mapper.Map<PaginatedList<HotelSearchResultResponse>>(searchResults);
        }

        private static Expression<Func<Hotel, bool>> BuildSearchExpression(SearchForHotelsQuery query)
        {
            return CreateSearchTermExpression(query)
                .And(CreateRoomTypeExpression(query))
                .And(CreateCapacityExpression(query))
                .And(CreatePriceRangeExpression(query))
                .And(CreateAmenitiesExpression(query))
                .And(CreateAvailableRoomsExpression(query))
                .And(CreateMinStarRatingExpression(query));
        }

        private static Expression<Func<Hotel, bool>> CreateSearchTermExpression(
            SearchForHotelsQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.SearchTerm))
                return _ => true;
            return h => h.Name.Contains(query.SearchTerm)
            || h.City.Name.Contains(query.SearchTerm)
            || h.City.Country.Contains(query.SearchTerm);
        }

        private static Expression<Func<Hotel, bool>> CreateRoomTypeExpression(
            SearchForHotelsQuery query)
        {
            if (query.RoomTypes.Any())
            {
                return h => h.RoomClasses.Any(rc => query.RoomTypes.Contains(rc.RoomType));
            }

            return _ => true;
        }

        private static Expression<Func<Hotel, bool>> CreateCapacityExpression(
            SearchForHotelsQuery query)
        {
            return h => h.RoomClasses.Any(rc =>
            rc.AdultsCapacity >= query.NumberOfAdults
            && rc.ChildrenCapacity >= query.NumberOfChildren);
        }

        private static Expression<Func<Hotel, bool>> CreatePriceRangeExpression(
            SearchForHotelsQuery query)
        {
            Expression<Func<Hotel, bool>> greaterThenMinPriceExpression =
                query.MinPrice.HasValue
                 ? h => h.RoomClasses.Any(rc => rc.PricePerNight >= query.MinPrice)
                 : _ => true;

            Expression<Func<Hotel, bool>> lessThenMaxPriceExpression =
               query.MaxPrice.HasValue
                ? h => h.RoomClasses.Any(rc => rc.PricePerNight <= query.MaxPrice)
                : _ => true;

            return greaterThenMinPriceExpression.And(lessThenMaxPriceExpression);

        }

        private static Expression<Func<Hotel, bool>> CreateAmenitiesExpression(SearchForHotelsQuery query)
        {
            if (query.Amenities.Any())
            {
                return h => query.Amenities.All(amenityId =>
                            h.RoomClasses.Any(rc => rc.Amenities.Any(a => a.Id == amenityId)));
            }

            return _ => true;
        }

        private static Expression<Func<Hotel, bool>> CreateAvailableRoomsExpression(
            SearchForHotelsQuery query)
        {
            return h => h.RoomClasses.Any(rc => rc.Rooms.Count(r =>
                !r.Bookings.Any(b => query.CheckOutDate <= b.CheckInDateUtc
                || query.CheckInDate >= b.CheckOutDateUtc)) >= query.NumberOfRooms);
        }

        private static Expression<Func<Hotel, bool>> 
            CreateMinStarRatingExpression(SearchForHotelsQuery query)
        {
            if (query.MinStarRating.HasValue)
            {
                return h => h.StarRating >= query.MinStarRating;
            }

            return _ => true;
        }
    }
}