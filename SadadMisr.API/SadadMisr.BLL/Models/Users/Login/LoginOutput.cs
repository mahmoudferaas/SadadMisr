using System;

namespace SadadMisr.BLL.Models.Users.Login
{
    public class LoginOutput
    {
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public DateTime? AccessTokenExpiryDate { get; set; }
        public string RefreshToken { get; set; }
        public bool Status { get; set; }
        public string[] Errors { get; set; }
    }
}