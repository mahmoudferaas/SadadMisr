using SadadMisr.DAL.Common;
using System;
using System.Collections.Generic;

namespace SadadMisr.DAL.Entities
{
    public class Bill : Entity<long>
    {
        public long ManifestId { get; set; }
        public int ShippingLineId { get; set; }
        public string BillNumber { get; set; }
        public int NumberOfContainers { get; set; }
        public string POL { get; set; }
        public string POD { get; set; }
        public string ACIDnumber { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerTaxNumber { get; set; }
        public string CustomerMobileNumber { get; set; }
        public int LineBillId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSentToAswaq { get; set; }
        public bool IsDone { get; set; }
        public string Status { get; set; }
        public string SCAC { get; set; }
        public string Containers20 { get; set; }
        public string Containers40 { get; set; }
     

        public Manifest Manifest { get; set; }
        public ShippingLine ShippingLine { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}