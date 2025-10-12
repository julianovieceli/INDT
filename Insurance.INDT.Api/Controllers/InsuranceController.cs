using INDT.Common.Insurance.Dto.Request;
using Insurance.INDT.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Proposal.INDT.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InsuranceController : ControllerBase
    {
        private readonly ILogger<InsuranceController> _logger;

        private readonly IInsuranceService _insuranceService;

        public InsuranceController(ILogger<InsuranceController> logger,
            IInsuranceService insuranceService)
        {
            _logger = logger;
            _insuranceService = insuranceService; 
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> GetInsurance([FromQuery] string name)
        {
            var result = await _insuranceService.GetByName(name);

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);
            }
            _logger.LogInformation("Ok");
            return Ok(result);
        }



        [HttpGet("fetch-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _insuranceService.GetAll();

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");

                if(result.ErrorCode == "404")
                    return NotFound("Empty");

                return BadRequest(result.ErrorCode);
            }
            _logger.LogInformation("Ok");
            return Ok(result);
        }



        [HttpPost(Name = "PostInsurance")]
        public async Task<IActionResult> RegisterInsurance(RegisterInsuranceDto registerInsurance) 
        {
             var result = await _insuranceService.Register(registerInsurance);

            if(result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);  
            }
            _logger.LogInformation("Ok");
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
