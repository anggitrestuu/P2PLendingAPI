using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P2PLendingAPI.DTOs;
using P2PLendingAPI.Services.Interfaces;

namespace P2PLendingAPI.Controllers
{
    [ApiController]
    [Route("api/lender")]
    [Authorize(Roles = "Lender")]
    public class LenderController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IUserService _userService;
        private readonly IFundingService _fundingService;

        public LenderController(ILoanService loanService, IUserService userService, IFundingService fundingService)
        {
            _loanService = loanService;
            _userService = userService;
            _fundingService = fundingService;
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized();

            var user = await _userService.GetByIdAsync(userId);
            return Ok(new { Balance = user.Balance });
        }

        [HttpGet("loan-requests")]
        public async Task<IActionResult> GetLoanRequests()
        {
            var loanRequests = await _loanService.GetLoanRequestsAsync();
            return Ok(loanRequests);
        }

        [HttpPost("fund-loan")]
        public async Task<IActionResult> FundLoan(FundLoanDto fundLoanDto)
        {
            var lenderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (lenderId == null)
                return Unauthorized();

            try
            {
                await _fundingService.FundLoanAsync(lenderId, fundLoanDto.LoanId, fundLoanDto.Amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("loan-history")]
        public async Task<IActionResult> GetLoanHistory()
        {
            var lenderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (lenderId == null)
                return Unauthorized();

            var loanHistory = await _loanService.GetLoanHistoryForLenderAsync(lenderId);
            return Ok(loanHistory);
        }
    }
}