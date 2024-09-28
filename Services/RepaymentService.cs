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
    public class RepaymentService : IRepaymentService
    {
        private readonly IRepaymentRepository _repaymentRepository;
        private readonly ILoanRepository _loanRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public RepaymentService(IRepaymentRepository repaymentRepository, ILoanRepository loanRepository, IUserRepository userRepository, IMapper mapper)
        {
            _repaymentRepository = repaymentRepository;
            _loanRepository = loanRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<RepaymentDto> GetByIdAsync(string id)
        {
            var repayment = await _repaymentRepository.GetByIdAsync(id);
            return _mapper.Map<RepaymentDto>(repayment);
        }

        public async Task<IEnumerable<RepaymentDto>> GetAllAsync()
        {
            var repayments = await _repaymentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RepaymentDto>>(repayments);
        }

        public async Task<IEnumerable<RepaymentDto>> GetRepaymentsByLoanIdAsync(string loanId)
        {
            var repayments = await _repaymentRepository.GetRepaymentsByLoanIdAsync(loanId);
            return _mapper.Map<IEnumerable<RepaymentDto>>(repayments);
        }

        public async Task<RepaymentDto> CreateAsync(CreateRepaymentDto createRepaymentDto)
        {
            var loan = await _loanRepository.GetByIdAsync(createRepaymentDto.LoanId);
            if (loan == null)
            {
                throw new KeyNotFoundException("Loan not found");
            }

            var borrower = await _userRepository.GetByIdAsync(loan.BorrowerId);
            if (borrower.Balance < createRepaymentDto.Amount)
            {
                throw new InvalidOperationException("Insufficient balance");
            }

            var totalRepaid = await _repaymentRepository.GetTotalRepaidForLoanAsync(loan.Id);
            var remainingAmount = loan.Amount + (loan.Amount * loan.InterestRate / 100) - totalRepaid;

            if (createRepaymentDto.Amount > remainingAmount)
            {
                throw new InvalidOperationException("Repayment amount exceeds the remaining balance");
            }

            var repayment = _mapper.Map<Repayment>(createRepaymentDto);
            repayment.Id = Guid.NewGuid().ToString();
            repayment.RepaidAmount = createRepaymentDto.Amount;
            repayment.BalanceAmount = remainingAmount - createRepaymentDto.Amount;
            repayment.RepaidStatus = repayment.BalanceAmount == 0 ? "full" : "partial";
            repayment.PaidAt = DateTime.UtcNow;

            var createdRepayment = await _repaymentRepository.CreateAsync(repayment);

            // Update borrower and lender balances
            borrower.Balance -= createRepaymentDto.Amount;
            await _userRepository.UpdateAsync(borrower);

            var lender = await _userRepository.GetByIdAsync(loan.LenderId);
            lender.Balance += createRepaymentDto.Amount;
            await _userRepository.UpdateAsync(lender);

            // Update loan status if fully repaid
            if (repayment.RepaidStatus == "full")
            {
                loan.Status = "repaid";
                await _loanRepository.UpdateAsync(loan);
            }

            return _mapper.Map<RepaymentDto>(createdRepayment);
        }

        public async Task RepayLoanAsync(string loanId, string borrowerId, decimal amount)
        {
            var loan = await _loanRepository.GetByIdAsync(loanId);
            if (loan == null)
                throw new KeyNotFoundException("Loan not found");

            if (loan.BorrowerId != borrowerId)
                throw new UnauthorizedAccessException("You are not authorized to repay this loan");

            if (loan.Status != "funded")
                throw new InvalidOperationException("This loan cannot be repaid at this time");

            var borrower = await _userRepository.GetByIdAsync(borrowerId);
            if (borrower.Balance < amount)
                throw new InvalidOperationException("Insufficient balance");

            var totalRepaid = await _repaymentRepository.GetTotalRepaidForLoanAsync(loanId);
            var remainingAmount = loan.Amount + (loan.Amount * loan.InterestRate / 100) - totalRepaid;

            if (amount > remainingAmount)
                throw new InvalidOperationException("Repayment amount exceeds the remaining balance");

            var repayment = new Repayment
            {
                Id = Guid.NewGuid().ToString(),
                LoanId = loanId,
                Amount = amount,
                RepaidAmount = amount,
                BalanceAmount = remainingAmount - amount,
                RepaidStatus = remainingAmount - amount == 0 ? "full" : "partial",
                PaidAt = DateTime.UtcNow
            };

            await _repaymentRepository.CreateAsync(repayment);

            borrower.Balance -= amount;
            await _userRepository.UpdateAsync(borrower);

            var lender = await _userRepository.GetByIdAsync(loan.LenderId);
            lender.Balance += amount;
            await _userRepository.UpdateAsync(lender);

            if (repayment.RepaidStatus == "full")
            {
                loan.Status = "repaid";
                await _loanRepository.UpdateAsync(loan);
            }
        }
    }
}