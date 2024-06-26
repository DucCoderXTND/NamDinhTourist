﻿namespace TND.Application.RoomClasses.GetById
{
    public record DiscountResponse(
        Guid Id,
        Guid RoomClassId,
        float Percentage,
        DateTime StartDateUtc,
        DateTime EndDateUtc,
        DateTime CreatedAtUtc);
}
