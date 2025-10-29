using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Request;
using INDT.Common.Insurance.Dto.Response;
using Insurance.INDT.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static BasicAuthenticationHandler;

namespace Insurance.Proposal.INDT.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = BasicAuthenticationOptions.DefaultScheme)]

    public class ProposalController : ControllerBase
    {
        private readonly ILogger<ProposalController> _logger;

        private readonly IProposalService _proposalService;

        public ProposalController(ILogger<ProposalController> logger,
            IProposalService proposalService)
        {
            _logger = logger;
            _proposalService = proposalService; 
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> GetProposal([FromQuery] int id)
        {
            var result = await _proposalService.GetById(id);

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);
            }
            _logger.LogInformation("Ok");
            return Ok(((Result < ProposalDto > )result).Value);
        }

        [HttpPost(Name = "PostProposal")]
        public async Task<IActionResult> RegisterProposal(RegisterProposalDto proposal)
        {
            var result = await _proposalService.Register(proposal);

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);
            }
            _logger.LogInformation("Ok");
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPatch(Name = "PatchStatus")]
        public async Task<IActionResult> UpdateStatus(UpdateProposalDto updateProposalDto)
        {
            var result = await _proposalService.UpdateStatus(updateProposalDto);

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);
            }
            _logger.LogInformation("Ok");
            return StatusCode(StatusCodes.Status200OK);
        }



        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _proposalService.GetAll();

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
