using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SadadMisr.DAL.Entities;

namespace SadadMisr.DAL.Configurations
{
    public class ShippingLineConfigurations : IEntityTypeConfiguration<ShippingLine>
    {
        public void Configure(EntityTypeBuilder<ShippingLine> builder)
        {

            builder.HasOne(d => d.ShippingAgency)
                 .WithMany()
                 .HasForeignKey(d => d.ShippingAgencyId);
        }
    }
}
