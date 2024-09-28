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
    public class FundingService : IFundingService
    {
        private readonly IFundingRepository _fundingRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public FundingService(IFundingRepository fundingRepository, ILoanRepository loanRepository, IUserRepository userRepository, IMapper mapper)
        {
            _fundingRepository = fundingRepository;
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<FundingDto> GetByIdAsync(string id)
        {
            var funding = await _fundingRepository.GetByIdAsync(id);
            return _mapper.Map<FundingDto>(funding);
        }

        public async Task<IEnumerable<FundingDto>> GetAllAsync()
        {
            var fundings = await _fundingRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FundingDto>>(fundings);
        }

        public async Task<IEnumerable<FundingDto>> GetFundingsByLenderIdAsync(string lenderId)
        {
            var fundings = await _fundingRepository.GetFundingsByLenderIdAsync(lenderId);
            return _mapper.Map<IEnumerable<FundingDto>>(fundings);
        }

        public async Task<FundingDto> CreateAsync(CreateFundingDto createFundingDto)
        {
            var loan = await _loanRepository.GetByIdAsync(createFundingDto.LoanId);
            if (loan == null)
            {
                throw new KeyNotFoundException("Loan not found");
            }

            var lender = await _userRepository.GetByIdAsync(createFundingDto.LenderId);
            if (lender == null)
            {
                throw new KeyNotFoundException("Lender not found");
            }

            if (lender.Balance < createFundingDto.Amount)
            {
                throw new InvalidOperationException("Insufficient balance");
            }

            var funding = _mapper.Map<Funding>(createFundingDto);
            funding.Id = Guid.NewGuid().ToString();
            funding.FundedAt = DateTime.UtcNow;

            var createdFunding = await _fundingRepository.CreateAsync(funding);

            // Update loan status
            loan.Status = "funded";
            await _loanRepository.UpdateAsync(loan);

            // Update lender and borrower balances
            lender.Balance -= createFundingDto.Amount;
            await _userRepository.UpdateAsync(lender);

            var borrower = await _userRepository.GetByIdAsync(loan.BorrowerId);
            borrower.Balance += createFundingDto.Amount;
            await _userRepository.UpdateAsync(borrower);

            return _mapper.Map<FundingDto>(createdFunding);
        }

        public async Task FundLoanAsync(string lenderId, string loanId, decimal amount)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
                throw new KeyNotFoundException("Loan not found");

            var lender = await _userRepository.GetByIdAsync(lenderId);
            if (lender == null)
                throw new KeyNotFoundException("Lender not found");

            if (lender.Balance < amount)
                throw new InvalidOperationException("Insufficient balance");

            if (loan.Status != "requested")
                throw new InvalidOperationException("Loan is not available for funding");

            if (amount != loan.Amount)
                throw new InvalidOperationException("Funding amount must match loan amount");

            var funding = new Funding
            {
                Id = Guid.NewGuid().ToString(),
                LoanId = loanId,
                LenderId = lenderId,
                Amount = amount,
                FundedAt = DateTime.UtcNow
            };

            await _fundingRepository.CreateAsync(funding);

            loan.Status = "funded";
            loan.LenderId = lenderId;
            loan.UpdatedAt = DateTime.UtcNow;
            await _loanRepository.UpdateAsync(loan);

            lender.Balance -= amount;
            await _userRepository.UpdateAsync(lender);

            var borrower = await _userRepository.GetByIdAsync(loan.BorrowerId);
            borrower.Balance += amount;
            await _userRepository.UpdateAsync(borrower);
        }
    }
}