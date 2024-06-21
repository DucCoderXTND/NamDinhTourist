using MediatR;
using TND.Domain.Enums;
using TND.Domain.Models;

namespace TND.Application.Hotels.Search
{
    public record SearchForHotelsQuery(
        string? SearchTerm,
        SortOrder? SortOrder,
        string? SortColumn,
        int PageNumber,
        int PageSize,
        DateOnly CheckInDate,
        DateOnly CheckOutDate,
        int NumberOfAdults,
        int NumberOfChildren,
        int NumberOfRooms,
        decimal? MinPrice,
        decimal? MaxPrice,
        int? MinStarRating,
        IEnumerable<RoomType> RoomTypes,
        IEnumerable<Guid> Amenities
        ) : IRequest<PaginatedList<HotelSearchResultResponse>>;
}
