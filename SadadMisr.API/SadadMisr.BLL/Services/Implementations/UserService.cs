using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models.Users.Create;
using SadadMisr.BLL.Models.Users.Login;
using SadadMisr.BLL.Models.Users.RefreshToken;
using SadadMisr.BLL.Services.Interfaces;
using SadadMisr.DAL;
using SadadMisr.DAL.Entities;
using SadadMisr.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ISadadMasrDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IIdentityService _identityService;

        public UserService(ISadadMasrDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager ,
            SignInManager<ApplicationUser> signInManager , IIdentityService identityService)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _identityService = identityService;
        }

        public async Task<Output<LoginOutput>> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(request.Email, SecurityHelper.Encrypt(request.Password), false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    #region get claims and roles

                    var userRoles = await _userManager.GetRolesAsync(user);

                    var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("username", user.UserName),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.AddRange(new[] { new Claim(ClaimTypes.Role, userRole) });
                    }

                    #endregion get claims and roles

                    #region get access token

                    var accessTokenDetails = _identityService.GenerateAccessToken(authClaims);

                    #endregion get access token

                    return new Output<LoginOutput>
                    {
                        Value = new LoginOutput()
                        {
                            UserId = user.Id,
                            AccessToken = accessTokenDetails.AccessToken,
                            AccessTokenExpiryDate = accessTokenDetails.AccessTokenDuration,
                            RefreshToken = _identityService.GenerateRefreshToken(),
                            Status = true
                        },
                    };
                }
                else if (result.IsLockedOut)
                {
                    return new Output<LoginOutput>
                    {
                        Value = new LoginOutput()
                        {
                            Status = false,
                            Errors = new string[] { "This account is locked for 10s, Please try again or contact with your administrator" }
                        },
                    };
                    
                }
                else
                {
                    return new Output<LoginOutput>
                    {
                        Value = new LoginOutput()
                        {
                            Status = false,
                            Errors = new string[] { "Incorrect Email/Username and Password" }
                        },
                    };
                }
            }
            else
            {
                return new Output<LoginOutput>
                {
                    Value = new LoginOutput()
                    {
                        Status = false,
                        Errors = new string[] { "This account is not exist, Please try again or contact with your administrator" }
                    },
                };
            }
        }

        
        public async Task<Output<bool>> Register(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                foreach (var item in request.Data)
                {
                    var user = _mapper.Map<ApplicationUser>(item);

                    string password = SecurityHelper.Encrypt(item.Password);

                    var result = await _userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        #region Create Role if not exist and assign to user

                        bool exist = await _roleManager.RoleExistsAsync(item.Role.ToString());

                        if (!exist)
                        {
                            var role = new ApplicationRole() { Id = Guid.NewGuid().ToString(), Name = item.Role.ToString() };

                            await _roleManager.CreateAsync(role);
                        }

                        await _userManager.AddToRoleAsync(user, item.Role.ToString());

                        #endregion Create Role if not exist and assign to user

                        return new Output<bool>(true);
                    }
                    else
                        return new Output<bool>
                        {
                            Errors = new[] { "DataBaseFailure.." }
                        };
                }

                return new Output<bool>(true);
            }
            catch (System.Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Add " + typeof(User).Name, ex.Message);
                return res;
                throw;
                throw;
            }
        }


        public async Task<Output<RefreshTokenOutput>> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var principal = _identityService.GetPrincipalFromExpiredToken(request.AccessToken);
            var username = principal.Claims.FirstOrDefault(c => c.Type == "username")?.Value;

            #region get claims and roles

            var user = await _userManager.FindByNameAsync(username);
            var authClaims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim("username", user.UserName),
                            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        };
            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                authClaims.AddRange(new[] { new Claim(ClaimTypes.Role, userRole) });
            }

            #endregion get claims and roles

            #region get access token

            var accessTokenDetails = _identityService.GenerateAccessToken(authClaims);

            #endregion get access token

            return new Output<RefreshTokenOutput>
            {
                Value = new RefreshTokenOutput()
                {
                    AccessToken = accessTokenDetails.AccessToken,
                    AccessTokenExpiryDate = accessTokenDetails.AccessTokenDuration,
                    RefreshToken = _identityService.GenerateRefreshToken(),
                    Status = true
                },
            };

        }



    }
}