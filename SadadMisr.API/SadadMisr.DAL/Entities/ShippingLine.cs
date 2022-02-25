using SadadMisr.DAL.Common;

namespace SadadMisr.DAL.Entities
{
    public class ShippingLine : LookupEntity<int>
    {
        public int ShippingAgencyId { get; set; }
        public ShippingAgency ShippingAgency { get; set; }
    }
}