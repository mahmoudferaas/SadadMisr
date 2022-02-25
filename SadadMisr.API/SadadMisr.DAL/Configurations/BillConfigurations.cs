using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SadadMisr.DAL.Entities;

namespace SadadMisr.DAL.Configurations
{
    public class BillConfigurations : IEntityTypeConfiguration<Bill>
    {
        public void Configure(EntityTypeBuilder<Bill> builder)
        {

            builder.HasOne(d => d.ShippingLine)
                 .WithMany()
                 .HasForeignKey(d => d.ShippingLineId);

            builder.Property(e => e.BillNumber).IsRequired();
            builder.Property(e => e.LineBillId).IsRequired();

        }
    }
}
