using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P2PLendingAPI.Data;
using P2PLendingAPI.Models;
using P2PLendingAPI.Repositories.Interfaces;

namespace P2PLendingAPI.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly ApplicationDbContext _context;

        public LoanRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Loan> GetByIdAsync(string id)
        {
            return await _context.Loans.FindAsync(id);
        }

        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _context.Loans.ToListAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByBorrowerIdAsync(string borrowerId)
        {
            return await _context.Loans.Where(l => l.BorrowerId == borrowerId).ToListAsync();
        }

        public async Task<Loan> CreateAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();
            return loan;
        }

        public async Task UpdateAsync(Loan loan)
        {
            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Loan>> GetLoansByStatusAsync(string status)
        {
            return await _context.Loans.Where(l => l.Status == status).ToListAsync();
        }


        public async Task<IEnumerable<Loan>> GetLoansByLenderIdAsync(string lenderId)
        {
            return await _context.Loans.Where(l => l.LenderId == lenderId).ToListAsync();
        }
    }
}