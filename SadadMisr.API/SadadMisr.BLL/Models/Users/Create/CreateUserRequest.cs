using SadadMisr.BLL.Models.Identity;
using System.Collections.Generic;

namespace SadadMisr.BLL.Models.Users.Create
{
    public class CreateUserRequest
    {
        public List<CreateUserModel> Data { get; set; }

    }
}