namespace TND.Application.Amenities.Common
{
    public record AmenityResponse(
        Guid Id,
        string Name,
        string? Description);
    
}
