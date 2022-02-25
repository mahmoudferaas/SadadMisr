using AutoMapper;
using SadadMisr.BLL.Mappings;
using SadadMisr.DAL.Entities;
using System;

namespace SadadMisr.BLL.Models
{
    public class InvoiceAswaq
    {
        public int id_pk { get; set; }

        public int bill_id { get; set; }
        public string invoice_type_name { get; set; }
        public string invoice_number { get; set; }
        public DateTime issue_date { get; set; }
        public string mobile_number { get; set; }
        public string bill_to_party { get; set; }
        public string tax_number { get; set; }
        public decimal items_amount { get; set; }
        public decimal vat_amount { get; set; }
        public decimal discount_amount { get; set; }
        public decimal total_amount { get; set; }
        public string invoice_currency { get; set; }
        public string is_paid { get; set; }
        public int line_invoice_id { get; set; }
        public string IsFixed { get; set; }

        public DateTime creation_date { get; set; }
        public string is_sent_to_aswaq { get; set; }
        public string is_done { get; set; }
        public string status { get; set; }
        public string is_deleted { get; set; }

    }
}