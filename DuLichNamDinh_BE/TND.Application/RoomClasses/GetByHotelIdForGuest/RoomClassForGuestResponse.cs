using TND.Application.RoomClasses.GetById;
using TND.Domain.Enums;

namespace TND.Application.RoomClasses.GetByHotelIdForGuest
{
    public record RoomClassForGuestResponse(
        Guid Id,
        string Name,
        int AdultsCapacity,
        int ChildrenCapacity,
        decimal PricePerNight,
        string? Description,
        RoomType RoomType,
        IEnumerable<Guid> Amenities,
        DiscountResponse? ActiveDiscount,
        IEnumerable<string> GalleryUrls);
}
