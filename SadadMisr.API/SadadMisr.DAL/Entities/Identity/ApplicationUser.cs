using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
namespace SadadMisr.DAL.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        //public string CreatedBy { get; set; }
        //public DateTime CreatedOn { get; set; }
        //public string UpdatedBy { get; set; }
        //public DateTime? UpdatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
