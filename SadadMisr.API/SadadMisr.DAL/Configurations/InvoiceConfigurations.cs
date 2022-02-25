using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SadadMisr.DAL.Entities;

namespace SadadMisr.DAL.Configurations
{
    public class InvoiceConfigurations : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasOne(d => d.Currency)
                 .WithMany()
                 .HasForeignKey(d => d.CurrencyId);

            //builder.HasOne(d => d.ShippingLine)
            //     .WithMany()
            //     .HasForeignKey(d => d.ShippingLineId);

            builder.Property(e => e.DiscountAmount).HasPrecision(18, 2);
            builder.Property(e => e.ItemsAmount).HasPrecision(18, 2);
            builder.Property(e => e.TotalAmount).HasPrecision(18, 2);
            builder.Property(e => e.VatAmount).HasPrecision(18, 2);

            builder.Property(e => e.BillId).IsRequired();
            builder.Property(e => e.InvoiceNumber).IsRequired();
            builder.Property(e => e.IssueDate).IsRequired();
            builder.Property(e => e.LineInvoiceId).IsRequired();

        }
    }
}
