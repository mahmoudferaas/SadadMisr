using AutoMapper;
using SadadMisr.BLL.Mappings;
using SadadMisr.DAL.Entities;
using System;

namespace SadadMisr.BLL.Models
{
    public class PaymentModel : IMapFrom<Payment>
    {
        public long? Id { get; set; }
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
        public bool IsDeleted { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<PaymentModel, Payment>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((_, __, srcMember) => srcMember != null));
        }
    }
}