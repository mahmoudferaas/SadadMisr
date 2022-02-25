using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SadadMisr.DAL.Entities;

namespace SadadMisr.DAL.Configurations
{
    public class PaymentConfigurations : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {

            builder.HasOne(d => d.Invoice)
                 .WithOne(p => p.Payment);

            builder.HasOne(d => d.Currency)
                 .WithMany()
                 .HasForeignKey(d => d.CurrencyId);

            builder.Property(e => e.TotalAmount).HasPrecision(18, 2);
            builder.Property(e => e.NetAmount).HasPrecision(18, 2);
            builder.Property(e => e.CommissionAmount).HasPrecision(18, 2);

            builder.Property(e => e.TotalAmount).IsRequired();
        }
    }
}
