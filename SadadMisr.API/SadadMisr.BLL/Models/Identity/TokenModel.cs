using System;

namespace SadadMisr.BLL.Models.Identity
{
    public class TokenModel
    {
        public string AccessToken { get; set; }
        public DateTime AccessTokenDuration { get; set; }
        public string RefreshToken { get; set; }
    }
}