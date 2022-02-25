using SadadMisr.BLL.Common;
using SadadMisr.BLL.Models.Users.Create;
using SadadMisr.BLL.Models.Users.Login;
using System.Threading;
using System.Threading.Tasks;

namespace SadadMisr.BLL.Services.Interfaces
{
    public interface IUserService
    {
        Task<Output<bool>> Register(CreateUserRequest request, CancellationToken cancellationToken);

        Task<Output<LoginOutput>> Login(LoginRequest request, CancellationToken cancellationToken);

        
    }
}