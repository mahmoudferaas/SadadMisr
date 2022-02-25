using AutoMapper;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Mappings;
using SadadMisr.DAL.Entities.Identity;

namespace SadadMisr.BLL.Models.Identity
{
    public class CreateUserModel : IMapFrom<ApplicationUser>
    {
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public Roles Role { get; set; }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateUserModel, ApplicationUser>()
                .ForMember(u => u.PasswordHash, opt => opt.MapFrom(u => SecurityHelper.Encrypt(u.Password)))
                .ForMember(u => u.IsDeleted, opt => opt.MapFrom(u => !u.IsActive))
                .ForMember(u => u.UserName, opt => opt.MapFrom(u => u.Email));
        }
    }
}