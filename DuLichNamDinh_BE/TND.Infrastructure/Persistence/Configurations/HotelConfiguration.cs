using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TND.Domain.Entities;

namespace TND.Infrastructure.Persistence.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.Thumbnail);
            builder.Ignore(x => x.Gallery);

            builder.HasMany(h => h.RoomClasses)
                .WithOne(r => r.Hotel)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(h => h.Bookings)
                .WithOne(b => b.Hotel)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(h => h.ReviewsRating)
               .HasPrecision(8, 6);

            builder.Property(h => h.Longitude)
              .HasPrecision(8, 6);

            builder.Property(h => h.Latitude)
              .HasPrecision(8, 6);
        }
    }
}
