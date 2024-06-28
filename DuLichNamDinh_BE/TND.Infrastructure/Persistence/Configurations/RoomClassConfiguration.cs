using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TND.Domain.Entities;
using TND.Domain.Enums;

namespace TND.Infrastructure.Persistence.Configurations
{
    public class RoomClassConfiguration : IEntityTypeConfiguration<RoomClass>
    {
        public void Configure(EntityTypeBuilder<RoomClass> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(rc => rc.Rooms)
                .WithOne(r => r.RoomClass)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(rc => rc.Amenities)
                .WithMany(a => a.RoomClasses);

            builder.Property(rc => rc.RoomType)
                .HasConversion(new EnumToStringConverter<RoomType>());

            builder.Ignore(rc => rc.Gallery);

            builder.Property(rc => rc.PricePerNight)
                .HasPrecision(18, 2);

        }
    }
}
