using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models.Users.Create;
using SadadMisr.BLL.Models.Users.Login;
using SadadMisr.BLL.Services.Interfaces;
using SadadMisr.DAL;
using SadadMisr.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public UserService(ISadadMasrDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Output<LoginOutput>> Login(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password))
                    return await Task.FromResult<Output<LoginOutput>>(null);

                request.Password = SecurityHelper.Encrypt(request.Password);

                // check if user exists
                var userDB = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Password == request.Password);

                if (userDB == null)
                    throw new NotFoundException(nameof(User) , userDB);

                var userDto = _mapper.Map<LoginOutput>(userDB);

                userDto.Token = GetToken(userDB);

                // authentication successful
                return new Output<LoginOutput> 
                {
                    Value = userDto,
                };
            }
            catch (Exception ex)
            {
                //return new LoginOutput { ErrorMessage = ex.Message};

                throw;
            }
        }

        public async Task<Output<bool>> Register(CreateUserRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var entities = _mapper.Map<List<User>>(request.Data);

                foreach (var item in entities)
                {
                    item.Password = SecurityHelper.Encrypt(item.Password);
                }

                await _context.Users.AddRangeAsync(entities, cancellationToken);

                if (await _context.SaveChangesAsync(cancellationToken) <= 0)
                {
                    return new Output<bool>
                    {
                        Errors = new[] { "DataBaseFailure.." }
                    };
                }

                return new Output<bool>(true);
            }
            catch (Exception ex)
            {
                var res = new Output<bool>(false);
                res.AddError("Add " + typeof(User).Name, ex.Message);
                return res;
                throw;
            }
        }

        private string GetToken(User userDB)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes("THIS IS USED TO SIGN AND VERIFY JWT TOKENS, REPLACE IT WITH YOUR OWN SECRET, IT CAN BE ANY STRING");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDB.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}