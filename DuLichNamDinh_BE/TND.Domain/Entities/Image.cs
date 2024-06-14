using TND.Domain.Enums;

namespace TND.Domain.Entities
{
    public class Image : EntityBase
    {
        public Guid EntityId { get; set; }
        public string Format { get; set; }
        public ImageType Type { get; set; }
        public string Path { get; set; }
    }
}
