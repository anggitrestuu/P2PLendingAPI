using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P2PLendingAPI.Services.Interfaces;
using System.Security.Claims;

namespace P2PLendingAPI.Controllers
{
    [ApiController]
    [Route("api/lender")]
    [Authorize(Roles = "Lender")]
    public class LenderController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IFundingService _fundingService;
        private readonly IUserService _userService;

        public LenderController(ILoanService loanService, IFundingService fundingService, IUserService userService)
        {
            _loanService = loanService;
            _fundingService = fundingService;
            _userService = userService;
        }

        [HttpGet("loan-requests")]
        public async Task<IActionResult> GetLoanRequests()
        {
            var loanRequests = await _loanService.GetLoanRequestsAsync();
            return Ok(loanRequests);
        }

        [HttpGet("fundings")]
        public async Task<IActionResult> GetLenderFundings()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var fundings = await _fundingService.GetFundingsByLenderIdAsync(userId);
            return Ok(fundings);
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetLenderBalance()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userService.GetByIdAsync(userId);
            return Ok(new { Balance = user.Balance });
        }
    }
}