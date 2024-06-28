using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TND.Domain.Entities;

namespace TND.Infrastructure.Persistence.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(c => c.Hotels)
                .WithOne(c => c.City)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(c => c.Thumbnail); //bỏ qua không lưu vào db
        }
    }
}
