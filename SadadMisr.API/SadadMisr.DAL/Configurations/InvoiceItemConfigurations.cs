using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SadadMisr.DAL.Entities;

namespace SadadMisr.DAL.Configurations
{
    public class InvoiceItemConfigurations : IEntityTypeConfiguration<InvoiceItem>
    {
        public void Configure(EntityTypeBuilder<InvoiceItem> builder)
        {
            builder.Property(e => e.ItemAmount).HasPrecision(18, 2);

            builder.Property(e => e.LineInvoiceId).IsRequired();
            builder.Property(e => e.ItemName).IsRequired();
            builder.Property(e => e.ItemAmount).IsRequired();
            builder.Property(e => e.LineInvoiceItemID).IsRequired(); 

        }
    }
}
