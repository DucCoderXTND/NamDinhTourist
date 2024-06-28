using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TND.Domain.Entities;

namespace TND.Infrastructure.Persistence.Configurations
{
    public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
    {
        public void Configure(EntityTypeBuilder<Amenity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(a => a.RoomClasses) //chỉ ra 1 amenity có n roomclass
                .WithMany(rc => rc.Amenities); //chỉ ra 1 roomclass có n amenity
        }
    }
}
