using System;

namespace SadadMisr.BLL.Models.Users.RefreshToken
{
    public class RefreshTokenOutput
    {
        public string AccessToken { get; set; }
        public DateTime? AccessTokenExpiryDate { get; set; }
        public string RefreshToken { get; set; }
        public bool Status { get; set; }
        public string[] Errors { get; set; }
    }
}