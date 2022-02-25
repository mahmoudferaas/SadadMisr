using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SadadMisr.DAL.Entities.Identity
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ICollection<ApplicationUserRole> UserRoles { get; set; }
    }
}
