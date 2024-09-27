using System.Collections.Generic;
using System.Threading.Tasks;
using P2PLendingAPI.DTOs;

namespace P2PLendingAPI.Services.Interfaces
{
    public interface ILoanService
    {
        Task<LoanDto> GetByIdAsync(string id);
        Task<IEnumerable<LoanDto>> GetAllAsync();
        Task<IEnumerable<LoanDto>> GetLoansByBorrowerIdAsync(string borrowerId);
        Task<LoanDto> CreateAsync(CreateLoanDto createLoanDto);
        Task UpdateStatusAsync(string id, UpdateLoanStatusDto updateLoanStatusDto);
        Task<IEnumerable<LoanDto>> GetLoanRequestsAsync();
    }
}