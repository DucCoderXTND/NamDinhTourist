using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TND.Application.Amenities.Common;
using TND.Application.RoomClasses.GetById;

namespace TND.Application.RoomClasses.GetForManagement
{
    public record RoomClassForManagementResponse(
        Guid RoomClassId,
        string Name,
        string? Description,
         int AdultsCapacity,
         int ChildrenCapacity,
        decimal PricePerNight,
        IEnumerable<AmenityResponse> Amenities,
        DiscountResponse? ActiveDiscount,
        DateTime CreatedAtUtc,
        DateTime? ModifiedAtUtc
        );
}
