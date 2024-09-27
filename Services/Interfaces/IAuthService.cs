using System.Threading.Tasks;
using P2PLendingAPI.DTOs;

namespace P2PLendingAPI.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(string email, string password);
        Task<UserDto> RegisterAsync(CreateUserDto createUserDto);
    }
}