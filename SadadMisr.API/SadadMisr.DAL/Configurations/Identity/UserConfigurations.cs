using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SadadMisr.DAL.Entities.Identity;

namespace SadadMisr.DAL.Configurations.Identity
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {

            //builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(16);
            //builder.HasIndex(x => x.PhoneNumber).IsUnique();
            builder.Property(x => x.Email).IsRequired().HasMaxLength(25);
            builder.HasIndex(x => x.Email).IsUnique();

            #region Filter

            builder.HasQueryFilter(s => !s.IsDeleted);

            #endregion Filter
        }
    }
}