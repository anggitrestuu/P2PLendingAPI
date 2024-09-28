using System.Collections.Generic;
using System.Threading.Tasks;
using P2PLendingAPI.DTOs;

namespace P2PLendingAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(string id);
        Task<UserDto> GetByEmailAsync(string email);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> CreateAsync(CreateUserDto createUserDto);
        Task UpdateAsync(string id, UpdateUserDto updateUserDto);
        Task DeleteAsync(string id);
        Task UpdateBalanceAsync(string id, decimal amount);
    }
}