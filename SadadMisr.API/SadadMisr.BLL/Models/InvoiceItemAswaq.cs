using AutoMapper;
using SadadMisr.BLL.Mappings;
using SadadMisr.DAL.Entities;
using System;

namespace SadadMisr.BLL.Models
{
    public class InvoiceItemAswaq
    {
        public long id { get; set; }
        public long id_pk { get; set; }
        public long invoice_id { get; set; }
        public long line_invoice_item_id { get; set; }
        public string item_name { get; set; }
        public decimal item_amount { get; set; }
        public long item_order { get; set; }
        public long is_deleted { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }

        
    }
}