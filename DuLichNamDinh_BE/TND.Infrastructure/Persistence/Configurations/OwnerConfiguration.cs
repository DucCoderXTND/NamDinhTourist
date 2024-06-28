using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TND.Domain.Entities;

namespace TND.Infrastructure.Persistence.Configurations
{
    public class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(o => o.Hotels)
                .WithOne(h => h.Owner)
                .HasForeignKey(h => h.OwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
