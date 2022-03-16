using SadadMisr.DAL.Common;
using System;
using System.Collections.Generic;

namespace SadadMisr.DAL.Entities
{
    public class Invoice : Entity<long>
    {
        public long BillId { get; set; }
        //public int? ShippingLineId { get; set; }
        //public int InvoiceTypeId { get; set; }
        public string InvoiceTypeName { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public string MobileNumber { get; set; }
        public string BillToParty { get; set; }
        public string TaxNumber { get; set; }
        public decimal ItemsAmount { get; set; }
        public decimal VatAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int? CurrencyId { get; set; }
        public string InvoiceCurrency { get; set; }
        public bool IsPaid { get; set; }
        public int LineInvoiceId { get; set; }
        public bool IsFixed { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSentToAswaq { get; set; }
        public bool IsDone { get; set; }
        public string Status { get; set; }

        public Payment Payment { get; set; }
        public Bill Bill { get; set; }
        public Currency Currency { get; set; }
        //public ShippingLine ShippingLine { get; set; }
        public ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}