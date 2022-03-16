using SadadMisr.DAL.Common;
using System;
using System.Collections.Generic;

namespace SadadMisr.DAL.Entities
{
    public class InvoiceItem : Entity<long>
    {
        public long InvoiceId { get; set; }
        public long LineInvoiceId { get; set; }
        public long LineInvoiceItemID { get; set; }
        public string ItemName { get; set; }
        public decimal ItemAmount { get; set; }
        public int ItemOrder { get; set; }
     
        public Invoice Invoice { get; set; }
    }
}