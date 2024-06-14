namespace TND.Domain.Entities
{
    public class Amenity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public ICollection<RoomClass> RoomClasses { get; set; } = new List<RoomClass>();
    }
}
