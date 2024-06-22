using TND.Application.Owners.Common;

namespace TND.Application.Hotels.GetHotelForManagementQuery
{
    public record HotelForManagementResponse(
        Guid Id,
        string Name,
        int StarRating,
        OwnerResponse Owner,
        int NumberOfRooms,
        DateTime CreatedAtUtc,
        DateTime? ModifiedAtUtc);
}
