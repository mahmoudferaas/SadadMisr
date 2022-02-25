using System.Linq;
using System.Threading.Tasks;

namespace SadadMisr.DAL.Context
{
    public class SadadMasrDbContextSeeding
    {
        public static async Task SeedData(SadadMasrDbContext erpDbContext)
        {

            if (!erpDbContext.Currencies.Any())
            {
                erpDbContext.Currencies.Add(new Entities.Currency
                {
                    Code = "Test",
                    Name = "SAR",
                });
            }

            if (!erpDbContext.ShippingLines.Any())
            {
                erpDbContext.ShippingLines.Add(new Entities.ShippingLine
                {
                    Code = "Test",
                    Name = "ShippingLine",
                    ShippingAgency = new Entities.ShippingAgency
                    {
                        Code = "Test",
                        Name = "ShippingAgency",
                    }
            });
            }

            //if (!erpDbContext.ShippingAgencies.Any())
            //{
            //    erpDbContext.ShippingAgencies.Add(new Entities.ShippingAgency
            //    {
            //        Code = "Test",
            //        Name = "ShippingAgency",
            //    });
            //}

            if (!erpDbContext.Ports.Any())
            {
                erpDbContext.Ports.Add(new Entities.Port
                {
                    Code = "Test",
                    Name = "Port",
                });
            }
            await erpDbContext.SaveChangesAsync();

            
        }
    }
}