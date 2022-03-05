using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SadadMisr.BLL.Models.Identity;
using SadadMisr.BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SadadMisr.BLL.Services.Implementations
{
    public class IdentityService : IIdentityService
    {
        private readonly IConfiguration _configuration;

        public IdentityService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenModel GenerateAccessToken(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            DateTime expireDate = new DateTimeOffset(DateTime.Now.AddMinutes(double.Parse(_configuration["JwtAuth:TokenLifetime"]))).DateTime;

            var token = new JwtSecurityToken(
              _configuration["JwtAuth:Issuer"],
              _configuration["JwtAuth:Audience"],
              claims: claims.ToArray(),
              expires: expireDate,
              signingCredentials: credentials);
            return new TokenModel()
            {
                AccessToken = "Bearer " + new JwtSecurityTokenHandler().WriteToken(token),
                AccessTokenDuration = expireDate
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            token = token.Replace("Bearer ", string.Empty);

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JwtAuth:Issuer"],
                ValidAudience = _configuration["JwtAuth:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtAuth:Key"])),
                ClockSkew = TimeSpan.Zero
            };
            //
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }
    }
}