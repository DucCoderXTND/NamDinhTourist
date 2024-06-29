using System.Linq.Expressions;
using TND.Domain.Entities;
using TND.Domain.Models;

namespace TND.Infrastructure.Persistence.Helpers
{
    public static class SortingExpressions
    {
        public static Expression<Func<Amenity, object>> GetAmenitySortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => a => a.Id,
                "name" => a => a.Name,
                _ => o => o.Id
            };
        }

        public static Expression<Func<Discount, object>> GetDiscountSortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => d => d.Id,
                "creationdate" => d => d.CreatedAtUtc,
                "startdate" => d => d.StartDateUtc,
                "enddate" => d => d.EndDateUtc,
                _ => o => o.Id
            };
        }

        public static Expression<Func<Room, object>> GetRoomSortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => d => d.Id,
                "number" => d => d.Number,
                _ => r => r.Id
            };
        }

        public static Expression<Func<RoomClass, object>> GetRoomClassSortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => d => d.Id,
                "name" => rc => rc.Name,
                "adultscapacity" => rc => rc.AdultsCapacity,
                "childrencapacity" => rc => rc.ChildrenCapacity,
                "pricepernight" => rc => rc.PricePerNight,
                _ => o => o.Id
            };
        }

        public static Expression<Func<Owner, object>> GetOwnerSortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => d => d.Id,
                "firstname" => d => d.FirstName,
                "lastname" => d => d.LastName,
                _ => r => r.Id
            };
        }

        public static Expression<Func<Hotel, object>> GetHotelSortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => d => d.Id,
                "name" => d => d.Name,
                _ => r => r.Id
            };
        }

        public static Expression<Func<HotelSearchResult, object>> GetHotelSearchSortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => d => d.Id,
                "number" => d => d.Name,
                "starrating" => h => h.StarRating,
                "price" => h => h.PricePerNightStartingAt,
                "reviewsrating" => h => h.ReviewsRating,
                _ => r => r.Id
            };
        }

        public static Expression<Func<City, object>> GetCitySortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => d => d.Id,
                "name" => d => d.Name,
                "country" => c => c.Country,
                "postoffice" => c => c.PostOffice,
                _ => r => r.Id
            };
        }

        public static Expression<Func<Review, object>> GetReviewSortExpression(string? sortColumn)
        {
            return sortColumn?.ToLower() switch
            {
                "id" => d => d.Id,
                "rating" => d => d.Rating,
                _ => r => r.Id
            };
        }

    }
}
