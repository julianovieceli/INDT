using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Dto.Request;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Proposal.INDT.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
            return Ok(result);
        }



        //[HttpGet("fetch-all")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var result = await _insuranceService.GetAll();

        //    if (result.IsFailure)
        //    {
        //        _logger.LogError($"Error{result.ErrorCode}");

        //        if(result.ErrorCode == "404")
        //            return NotFound("Empty");

        //        return BadRequest(result.ErrorCode);
        //    }
        //    _logger.LogInformation("Ok");
        //    return Ok(result);
        //}



        //[HttpPost(Name = "PostInsurance")]
        //public async Task<IActionResult> RegisterInsurance(RegisterInsuranceDto registerInsurance) 
        //{
        //     var result = await _insuranceService.Register(registerInsurance);

        //    if(result.IsFailure)
        //    {
        //        _logger.LogError($"Error{result.ErrorCode}");
        //        return BadRequest(result);  
        //    }
        //    _logger.LogInformation("Ok");
        //    return StatusCode(StatusCodes.Status201Created);
        //}
    }
}
