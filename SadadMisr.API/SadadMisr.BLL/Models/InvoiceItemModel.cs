using AutoMapper;
using SadadMisr.BLL.Mappings;
using SadadMisr.DAL.Entities;
using System;

namespace SadadMisr.BLL.Models
{
    public class InvoiceItemModel : IMapFrom<InvoiceItem>
    {
        public long? Id { get; set; }
        public long InvoiceId { get; set; }
        public long LineInvoiceId { get; set; }
        public long LineInvoiceItemID { get; set; }
        public string ItemName { get; set; }
        public decimal ItemAmount { get; set; }
        public int ItemOrder { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<InvoiceItemModel, InvoiceItem>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((_, __, srcMember) => srcMember != null));
        }
    }
}