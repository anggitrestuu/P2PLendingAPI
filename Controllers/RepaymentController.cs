using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P2PLendingAPI.DTOs;
using P2PLendingAPI.Services.Interfaces;

namespace P2PLendingAPI.Controllers
{
    [ApiController]
    [Route("api/repayment")]
    [Authorize]
    public class RepaymentController : ControllerBase
    {
        private readonly IRepaymentService _repaymentService;

        public RepaymentController(IRepaymentService repaymentService)
        {
            _repaymentService = repaymentService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRepayment(string id)
        {
            var repayment = await _repaymentService.GetByIdAsync(id);
            if (repayment == null)
            {
                return NotFound();
            }
            return Ok(repayment);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRepayments()
        {
            var repayments = await _repaymentService.GetAllAsync();
            return Ok(repayments);
        }

        [HttpPost]
        [Authorize(Roles = "Borrower")]
        public async Task<IActionResult> CreateRepayment(CreateRepaymentDto createRepaymentDto)
        {
            var repayment = await _repaymentService.CreateAsync(createRepaymentDto);
            return CreatedAtAction(nameof(GetRepayment), new { id = repayment.Id }, repayment);
        }
    }
}