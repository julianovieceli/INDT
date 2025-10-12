using INDT.Common.Insurance.Dto.Request;
using Insurance.INDT.Application.Services;
using Insurance.ProposalHire.INDT.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.HireProposal.INDT.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProposalHireController : ControllerBase
    {
    
        private readonly ILogger<ProposalHireController> _logger;
        private readonly IProposalHireService _proposalHireService;

        public ProposalHireController(ILogger<ProposalHireController> logger,
            IProposalHireService proposalHireService)
        {
            _logger = logger;
            _proposalHireService = proposalHireService;
        }


        [HttpPost("hire")]
        public async Task<IActionResult> Hire(HireProposalDto hireProposalDto)
        {
            var result = await _proposalHireService.Register(hireProposalDto);

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);
            }
            _logger.LogInformation("Ok");
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _proposalHireService.GetAll();

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");

                if (result.ErrorCode == "404")
                    return NotFound("Empty");

                return BadRequest(result.ErrorCode);
            }
            _logger.LogInformation("Ok");
            return Ok(result);
        }


    }
}
