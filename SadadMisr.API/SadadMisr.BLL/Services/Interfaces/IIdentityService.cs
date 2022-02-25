using SadadMisr.BLL.Models.Identity;
using System.Collections.Generic;
using System.Security.Claims;

namespace SadadMisr.BLL.Services.Interfaces
{
    public interface IIdentityService
    {
        public TokenModel GenerateAccessToken(List<Claim> claims);

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);

        public string GenerateRefreshToken();
    }
}