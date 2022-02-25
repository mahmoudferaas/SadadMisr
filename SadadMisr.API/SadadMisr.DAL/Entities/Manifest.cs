using SadadMisr.DAL.Common;
using System;
using System.Collections.Generic;

namespace SadadMisr.DAL.Entities
{
    public class Manifest : Entity<long>
    {
        public string VesselName { get; set; }
        public string VoyageNumber { get; set; }
        public DateTime EstimatedDate { get; set; }
        public int? ShippingAgencyId { get; set; }
        public int LineManifestId { get; set; }
        public string CallPort { get; set; }
        public int NumberOfBills { get; set; }
        public bool IsExport { get; set; }
        public int? ShippingLineId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSentToAswaq { get; set; }
        public bool IsDone { get; set; }
        public string Status { get; set; }

        public ShippingLine ShippingLine { get; set; }
        public ShippingAgency ShippingAgency { get; set; }
        public ICollection<Bill> Bills { get; set; }
    }
}