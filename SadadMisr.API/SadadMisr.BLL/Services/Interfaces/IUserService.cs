using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models.Users.Create;
using SadadMisr.BLL.Models.Users.Login;
using SadadMisr.BLL.Models.Users.RefreshToken;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<Output<bool>> Register(CreateUserRequest request, CancellationToken cancellationToken);

        Task<Output<LoginOutput>> Login(LoginRequest request, CancellationToken cancellationToken);
        Task<Output<RefreshTokenOutput>> RefreshToken(RefreshTokenRequest request, CancellationToken cancellationToken);


    }
}