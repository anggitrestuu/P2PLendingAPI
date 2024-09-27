using System.Collections.Generic;
using System.Threading.Tasks;
using P2PLendingAPI.Models;

namespace P2PLendingAPI.Repositories.Interfaces
{
    public interface IRepaymentRepository
    {
        Task<Repayment> GetByIdAsync(string id);
        Task<IEnumerable<Repayment>> GetAllAsync();
        Task<IEnumerable<Repayment>> GetRepaymentsByLoanIdAsync(string loanId);
        Task<Repayment> CreateAsync(Repayment repayment);
        Task<decimal> GetTotalRepaidForLoanAsync(string loanId);
    }
}
