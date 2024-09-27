using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using P2PLendingAPI.DTOs;
using P2PLendingAPI.Models;
using P2PLendingAPI.Repositories.Interfaces;
using P2PLendingAPI.Services.Interfaces;

namespace P2PLendingAPI.Services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IMapper _mapper;

        public LoanService(ILoanRepository loanRepository, IMapper mapper)
        {
            _loanRepository = loanRepository;
            _mapper = mapper;
        }

        public async Task<LoanDto> GetByIdAsync(string id)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            return _mapper.Map<LoanDto>(loan);
        }

        public async Task<IEnumerable<LoanDto>> GetAllAsync()
        {
            var loans = await _loanRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        public async Task<IEnumerable<LoanDto>> GetLoansByBorrowerIdAsync(string borrowerId)
        {
            var loans = await _loanRepository.GetLoansByBorrowerIdAsync(borrowerId);
            return _mapper.Map<IEnumerable<LoanDto>>(loans);
        }

        public async Task<LoanDto> CreateAsync(CreateLoanDto createLoanDto)
        {
            var loan = _mapper.Map<Loan>(createLoanDto);
            loan.Id = Guid.NewGuid().ToString();
            loan.Status = "requested";
            loan.CreatedAt = DateTime.UtcNow;
            loan.UpdatedAt = DateTime.UtcNow;
            loan.Duration = 12; // Fixed 12 months duration

            var createdLoan = await _loanRepository.CreateAsync(loan);
            return _mapper.Map<LoanDto>(createdLoan);
        }

        public async Task UpdateStatusAsync(string id, UpdateLoanStatusDto updateLoanStatusDto)
        {
            var loan = await _loanRepository.GetByIdAsync(id);
            if (loan == null)
            {
                throw new KeyNotFoundException("Loan not found");
            }

            loan.Status = updateLoanStatusDto.Status;
            loan.UpdatedAt = DateTime.UtcNow;

            await _loanRepository.UpdateAsync(loan);
        }

        public async Task<IEnumerable<LoanDto>> GetLoanRequestsAsync()
        {
            var loanRequests = await _loanRepository.GetLoansByStatusAsync("requested");
            return _mapper.Map<IEnumerable<LoanDto>>(loanRequests);
        }
    }
}