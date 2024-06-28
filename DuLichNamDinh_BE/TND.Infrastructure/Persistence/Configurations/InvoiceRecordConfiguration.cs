using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TND.Domain.Entities;

namespace TND.Infrastructure.Persistence.Configurations
{
    public class InvoiceRecordConfiguration : IEntityTypeConfiguration<InvoiceRecord>
    {
        public void Configure(EntityTypeBuilder<InvoiceRecord> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(ir => ir.PriceAtBooking)
                .HasPrecision(18, 2);

            builder.Property(ir => ir.DiscountPercentageAtBooking)
              .HasPrecision(18, 2);
        }
    }
}
