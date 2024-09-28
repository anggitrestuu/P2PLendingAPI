using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P2PLendingAPI.Services.Interfaces;
using P2PLendingAPI.DTOs;
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

        [HttpPost("request-loan")]
        public async Task<IActionResult> RequestLoan(CreateLoanDto createLoanDto)
        {
            var borrowerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (borrowerId == null)
                return Unauthorized();

            createLoanDto.BorrowerId = borrowerId;

            // get lender by LenderEmail
            var lender = await _userService.GetByEmailAndRoleAsync(createLoanDto.LenderEmail, "Lender");
            if (lender == null)
                return BadRequest("Lender not found");

            createLoanDto.LenderId = lender.Id;
            createLoanDto.Status = "Requested";


            try
            {
                var loan = await _loanService.CreateLoanRequestAsync(createLoanDto);
                return CreatedAtAction(nameof(GetLoanDetails), new { id = loan.Id }, loan);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("loans/{id}")]
        public async Task<IActionResult> GetLoanDetails(string id)
        {
            var loan = await _loanService.GetByIdAsync(id);
            if (loan == null)
                return NotFound();

            var borrowerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (borrowerId != loan.BorrowerId)
                return Forbid();

            return Ok(loan);
        }

        [HttpPost("loans/{id}/repay")]
        public async Task<IActionResult> RepayLoan(string id, RepayLoanDto repayLoanDto)
        {
            var borrowerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (borrowerId == null)
                return Unauthorized();

            try
            {
                await _repaymentService.RepayLoanAsync(id, borrowerId, repayLoanDto.Amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}