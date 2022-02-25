using SadadMisr.DAL.Common;

namespace SadadMisr.DAL.Entities
{
    public class User : Entity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}