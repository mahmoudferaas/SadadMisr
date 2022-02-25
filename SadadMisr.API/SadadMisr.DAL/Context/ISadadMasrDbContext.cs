using Microsoft.EntityFrameworkCore;
using SadadMisr.DAL.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.DAL
{
    public interface ISadadMasrDbContext
    {
        DbSet<Manifest> Manifests { get; set; }
        DbSet<Bill> Bills { get; set; }
        DbSet<Invoice> Invoices { get; set; }
        DbSet<Payment> Payments { get; set; }
        DbSet<Currency> Currencies { get; set; }
        DbSet<ShippingLine> ShippingLines { get; set; }
        DbSet<ShippingAgency> ShippingAgencies { get; set; }
        DbSet<Port> Ports { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}