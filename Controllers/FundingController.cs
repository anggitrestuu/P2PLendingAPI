using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using P2PLendingAPI.DTOs;
using P2PLendingAPI.Services.Interfaces;

namespace P2PLendingAPI.Controllers
{
    [ApiController]
    [Route("api/funding")]
    [Authorize]
    public class FundingController : ControllerBase
    {
        private readonly IFundingService _fundingService;

        public FundingController(IFundingService fundingService)
        {
            _fundingService = fundingService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFunding(string id)
        {
            var funding = await _fundingService.GetByIdAsync(id);
            if (funding == null)
            {
                return NotFound();
            }
            return Ok(funding);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFundings()
        {
            var fundings = await _fundingService.GetAllAsync();
            return Ok(fundings);
        }

        [HttpPost]
        [Authorize(Roles = "Lender")]
        public async Task<IActionResult> CreateFunding(CreateFundingDto createFundingDto)
        {
            var funding = await _fundingService.CreateAsync(createFundingDto);
            return CreatedAtAction(nameof(GetFunding), new { id = funding.Id }, funding);
        }
    }
}