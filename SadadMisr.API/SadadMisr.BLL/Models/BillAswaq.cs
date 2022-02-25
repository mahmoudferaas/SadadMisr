using AutoMapper;
using SadadMisr.BLL.Mappings;
using SadadMisr.DAL.Entities;
using System;
using System.Collections.Generic;

namespace SadadMisr.BLL.Models
{
    public class BillAswaq
    {
        //public int id { get; set; }
        public int id_pk { get; set; }
        public int manifest_id { get; set; }
        public int shipping_line_id { get; set; }
        public string bill_number { get; set; }
        public int number_of_containers { get; set; }
        public string pol { get; set; }
        public string pod { get; set; }
        public string aci_dnumber { get; set; }
        public int customer_id { get; set; }
        public string customer_tax_number { get; set; }
        public string customer_mobile_number { get; set; }
        public string scac { get; set; }
        public string customer_name { get; set; }
        public int line_bill_id { get; set; }
        public DateTime creation_date { get; set; }
        public string is_sent_to_aswaq { get; set; }
        public string is_done { get; set; }
        public string status { get; set; }
        public string is_deleted { get; set; }
        public string containers_20 { get; set; }
        public string containers_40 { get; set; }
        public string customer_email { get; set; }
 
    }
}