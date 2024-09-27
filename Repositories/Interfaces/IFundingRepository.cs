using System.Collections.Generic;
using System.Threading.Tasks;
using P2PLendingAPI.Models;

namespace P2PLendingAPI.Repositories.Interfaces
{
    public interface IFundingRepository
    {
        Task<Funding> GetByIdAsync(string id);
        Task<IEnumerable<Funding>> GetAllAsync();
        Task<IEnumerable<Funding>> GetFundingsByLenderIdAsync(string lenderId);
        Task<Funding> CreateAsync(Funding funding);
    }
}