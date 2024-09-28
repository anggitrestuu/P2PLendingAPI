using System.Collections.Generic;
using System.Threading.Tasks;
using P2PLendingAPI.Models;

namespace P2PLendingAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(string id);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string id);
        Task<User> GetByEmailAndRoleAsync(string email, string role);
    }
}