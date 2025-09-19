using Application.DTOs.Auth;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<DataUserDto> RegisterAsync(RegisterDto model);
        Task<DataUserDto> LoginAsync(LoginDto model);
        Task<DataUserDto> RefreshTokenAsync(string refreshToken);
    }
}
