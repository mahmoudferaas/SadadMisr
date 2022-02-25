using Microsoft.EntityFrameworkCore;
using SadadMisr.DAL.Entities;


namespace SadadMisr.DAL
{
    public class SadadMasrDbContext : DbContext , ISadadMasrDbContext
    {
        public SadadMasrDbContext(DbContextOptions<SadadMasrDbContext> options) : base(options)
        {
        }

        public DbSet<Manifest> Manifests { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ShippingLine> ShippingLines { get; set; }
        public DbSet<ShippingAgency> ShippingAgencies { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SadadMasrDbContext).Assembly);
        }
    }
}