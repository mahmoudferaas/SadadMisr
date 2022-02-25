using AutoMapper;
using SadadMisr.BLL.Mappings;
using SadadMisr.DAL.Entities;
using System;
using System.Collections.Generic;

namespace SadadMisr.BLL.Models
{
    public class ManifestAswaq 
    {
       
        //public int id { get; set; }
        public int id_pk { get; set; }
        public string vessel_name { get; set; }
        public string voyage_number { get; set; }
        public DateTime estimated_date { get; set; }
        public int shipping_agency_id { get; set; }
        public int line_manifest_id { get; set; }
        public string call_port { get; set; }
        public int number_of_bills { get; set; }
        public string is_export { get; set; }
        public int shipping_line_id { get; set; }
        public string is_sent_to_aswaq { get; set; }
        public string is_done { get; set; }
        public string status { get; set; }
        public string is_deleted { get; set; }
 
    }
}