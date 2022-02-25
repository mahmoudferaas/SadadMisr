using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models;
using SadadMisr.BLL.Models.Bills.Create;
using SadadMisr.BLL.Models.Bills.Delete;
using SadadMisr.BLL.Models.Bills.GetById;
using SadadMisr.BLL.Models.Bills.Update;
using SadadMisr.BLL.Models.Users.Create;
using SadadMisr.BLL.Models.Users.Login;
using SadadMisr.BLL.Models.Users.RefreshToken;
using SadadMisr.BLL.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost("Login")]
        public async Task<Output<LoginOutput>> Login([FromBody] LoginRequest command, CancellationToken cancellationToken)
        {
            return await _userService.Login(command ,cancellationToken );
        }

        [Authorize]
        [HttpPost("RefreshToken")]
        public async Task<Output<RefreshTokenOutput>> RefreshToken([FromBody] RefreshTokenRequest command, CancellationToken cancellationToken)
        {
            return await _userService.RefreshToken(command, cancellationToken);
        }


        [HttpPost("Register")]
        public async Task<Output<bool>> Register([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
        {
            return await _userService.Register(request, cancellationToken);
        }

        
    }
}