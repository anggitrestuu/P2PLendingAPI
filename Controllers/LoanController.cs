using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P2PLendingAPI.DTOs;
using P2PLendingAPI.Services.Interfaces;

namespace P2PLendingAPI.Controllers
{
    [ApiController]
    [Route("api/loan")]
    [Authorize]
    public class LoanController : ControllerBase
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLoan(string id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return Ok(loan);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllLoans()
        {
            var loans = await _loanService.GetAllAsync();
            return Ok(loans);
        }

        [HttpPost]
        [Authorize(Roles = "Borrower")]
        public async Task<IActionResult> CreateLoan(CreateLoanDto createLoanDto)
        {
            var loan = await _loanService.CreateAsync(createLoanDto);
            return CreatedAtAction(nameof(GetLoan), new { id = loan.Id }, loan);
        }

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Lender")]
        public async Task<IActionResult> UpdateLoanStatus(string id, UpdateLoanStatusDto updateLoanStatusDto)
        {
            await _loanService.UpdateStatusAsync(id, updateLoanStatusDto);
            return NoContent();
        }
    }
}