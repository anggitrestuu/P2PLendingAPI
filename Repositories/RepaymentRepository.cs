using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P2PLendingAPI.Data;
using P2PLendingAPI.Models;
using P2PLendingAPI.Repositories.Interfaces;

namespace P2PLendingAPI.Repositories
{
    public class RepaymentRepository : IRepaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public RepaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Repayment> GetByIdAsync(string id)
        {
            return await _context.Repayments.FindAsync(id);
        }

        public async Task<IEnumerable<Repayment>> GetAllAsync()
        {
            return await _context.Repayments.ToListAsync();
        }

        public async Task<IEnumerable<Repayment>> GetRepaymentsByLoanIdAsync(string loanId)
        {
            return await _context.Repayments.Where(r => r.LoanId == loanId).ToListAsync();
        }

        public async Task<Repayment> CreateAsync(Repayment repayment)
        {
            await _context.Repayments.AddAsync(repayment);
            await _context.SaveChangesAsync();
            return repayment;
        }

        public async Task<decimal> GetTotalRepaidForLoanAsync(string loanId)
        {
            return await _context.Repayments
                .Where(r => r.LoanId == loanId)
                .SumAsync(r => r.Amount);
        }
    }
}