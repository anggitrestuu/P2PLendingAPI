using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using P2PLendingAPI.Data;
using P2PLendingAPI.Models;
using P2PLendingAPI.Repositories.Interfaces;

namespace P2PLendingAPI.Repositories
{
    public class FundingRepository : IFundingRepository
    {
        private readonly ApplicationDbContext _context;

        public FundingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Funding> GetByIdAsync(string id)
        {
            return await _context.Fundings.FindAsync(id);
        }

        public async Task<IEnumerable<Funding>> GetAllAsync()
        {
            return await _context.Fundings.ToListAsync();
        }

        public async Task<IEnumerable<Funding>> GetFundingsByLenderIdAsync(string lenderId)
        {
            return await _context.Fundings.Where(f => f.LenderId == lenderId).ToListAsync();
        }

        public async Task<Funding> CreateAsync(Funding funding)
        {
            await _context.Fundings.AddAsync(funding);
            await _context.SaveChangesAsync();
            return funding;
        }
    }
}