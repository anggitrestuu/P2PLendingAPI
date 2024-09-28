using System.Collections.Generic;
using System.Threading.Tasks;
using P2PLendingAPI.DTOs;

namespace P2PLendingAPI.Services.Interfaces
{
    public interface IFundingService
    {
        Task<FundingDto> GetByIdAsync(string id);
        Task<IEnumerable<FundingDto>> GetAllAsync();
        Task<IEnumerable<FundingDto>> GetFundingsByLenderIdAsync(string lenderId);
        Task<FundingDto> CreateAsync(CreateFundingDto createFundingDto);
        Task FundLoanAsync(string lenderId, string loanId, decimal amount);
    }
}