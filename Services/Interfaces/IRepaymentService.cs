using System.Collections.Generic;
using System.Threading.Tasks;
using P2PLendingAPI.DTOs;

namespace P2PLendingAPI.Services.Interfaces
{
    public interface IRepaymentService
    {
        Task<RepaymentDto> GetByIdAsync(string id);
        Task<IEnumerable<RepaymentDto>> GetAllAsync();
        Task<IEnumerable<RepaymentDto>> GetRepaymentsByLoanIdAsync(string loanId);
        Task<RepaymentDto> CreateAsync(CreateRepaymentDto createRepaymentDto);
        Task RepayLoanAsync(string loanId, string borrowerId, decimal amount);
    }
}