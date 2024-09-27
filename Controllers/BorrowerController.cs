using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P2PLendingAPI.Services.Interfaces;
using System.Security.Claims;

namespace P2PLendingAPI.Controllers
{
    [ApiController]
    [Route("api/borrower")]
    [Authorize(Roles = "Borrower")]
    public class BorrowerController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IRepaymentService _repaymentService;
        private readonly IUserService _userService;

        public BorrowerController(ILoanService loanService, IRepaymentService repaymentService, IUserService userService)
        {
            _loanService = loanService;
            _repaymentService = repaymentService;
            _userService = userService;
        }

        [HttpGet("loans")]
        public async Task<IActionResult> GetBorrowerLoans()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var loans = await _loanService.GetLoansByBorrowerIdAsync(userId);
            return Ok(loans);
        }

        [HttpGet("loans/{loanId}/repayments")]
        public async Task<IActionResult> GetLoanRepayments(string loanId)
        {
            var repayments = await _repaymentService.GetRepaymentsByLoanIdAsync(loanId);
            return Ok(repayments);
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBorrowerBalance()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetByIdAsync(userId);
            return Ok(new { Balance = user.Balance });
        }
    }
}