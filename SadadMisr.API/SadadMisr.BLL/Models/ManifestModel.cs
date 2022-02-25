using AutoMapper;
using SadadMisr.BLL.Mappings;
using SadadMisr.DAL.Entities;
using System;
using System.Collections.Generic;

namespace SadadMisr.BLL.Models
{
    public class ManifestModel : IMapFrom<Manifest>
    {
        public long? Id { get; set; }
        public string VesselName { get; set; }
        public string VoyageNumber { get; set; }
        public DateTime EstimatedDate { get; set; }
        public int? ShippingAgencyId { get; set; }
        public int LineManifestId { get; set; }
        public string CallPort { get; set; }
        public int NumberOfBills { get; set; }
        public bool IsExport { get; set; }
        public int? ShippingLineId { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsSentToAswaq { get; set; }
        public bool IsDone { get; set; }
        public string Status { get; set; }
        //public ICollection<BillModel> Bills { get; set; }

        public string ShippingAgencyCode { get; set; }
        public string ShippingLineCode { get; set; }
        public bool IsDeleted { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ManifestModel, Manifest>().ReverseMap()
                .ForAllMembers(opts => opts.Condition((_, __, srcMember) => srcMember != null));
        }
    }
}