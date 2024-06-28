using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TND.Domain.Entities;
using TND.Domain.Enums;

namespace TND.Infrastructure.Persistence.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(b => b.Rooms)
                .WithMany(r => r.Bookings);

            builder.HasMany(b => b.Invoice)
                .WithOne(ir => ir.Booking)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            //Khi bạn lưu một đối tượng với PaymentMethod là CreditCard,
            //giá trị "CreditCard" sẽ được lưu vào cơ sở dữ liệu. Khi bạn truy xuất đối tượng đó,
            //chuỗi "CreditCard" sẽ được chuyển đổi lại thành giá trị PaymentMethod.CreditCard.

            builder.Property(b => b.PaymentMethod)
                 .HasConversion(new EnumToStringConverter<PaymentMethod>());

            builder.Property(b => b.TotalPrice)
                .HasPrecision(18, 2);
        }
    }
}
