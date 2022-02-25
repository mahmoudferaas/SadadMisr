using SadadMisr.DAL.Common;
using System;

namespace SadadMisr.DAL.Entities
{
    public class Payment : Entity<long>
    {
        public long InvoiceId { get; set; }
        public string AswaqPaymentId { get; set; }
        public int TransactionId { get; set; }
        public string TransactionNumber { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? CurrencyId { get; set; }
        public int LinePaymentId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSentToLine { get; set; }
        public bool IsDone { get; set; }
        public string Status { get; set; }

        public Invoice Invoice { get; set; }
        public Currency Currency { get; set; }
    }
}