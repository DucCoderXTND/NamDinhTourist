namespace TND.Application.Hotels.GetForGuestById
{
    public record HotelForGuestResponse(
        Guid Id,
        string Name,
        double Longitude,
        double Latitude,
        string CityName,
        string CountryName,
        int StarRating,
        double ReviewsRating,
        string? BriefDescription,
        string? Description,
        string? ThumbnailUrl,
        IEnumerable<string> GalleryUrls);
}
