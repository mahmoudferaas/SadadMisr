using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SadadMisr.DAL.Entities;

namespace SadadMisr.DAL.Configurations
{
    public class ManifestConfigurations : IEntityTypeConfiguration<Manifest>
    {
        public void Configure(EntityTypeBuilder<Manifest> builder)
        {
            builder.HasOne(d => d.ShippingAgency)
                 .WithMany()
                 .HasForeignKey(d => d.ShippingAgencyId);

            builder.HasOne(d => d.ShippingLine)
                 .WithMany()
                 .HasForeignKey(d => d.ShippingLineId);

            builder.Property(e => e.VesselName).IsRequired();
            builder.Property(e => e.VoyageNumber).IsRequired();
        }
    }
}
