using System.Collections.Generic;
using System.Threading.Tasks;
using P2PLendingAPI.Models;

namespace P2PLendingAPI.Repositories.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan> GetByIdAsync(string id);
        Task<IEnumerable<Loan>> GetAllAsync();
        Task<IEnumerable<Loan>> GetLoansByBorrowerIdAsync(string borrowerId);
        Task<Loan> CreateAsync(Loan loan);
        Task UpdateAsync(Loan loan);
        Task<IEnumerable<Loan>> GetLoansByStatusAsync(string status);
        Task<IEnumerable<Loan>> GetLoansByLenderIdAsync(string lenderId);
    }
}