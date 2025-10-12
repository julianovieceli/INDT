using INDT.Common.Insurance.Dto.Request;
using Insurance.INDT.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.Proposal.INDT.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ILogger<ClientController> _logger;

        private readonly IClientService _clientService;

        public ClientController(ILogger<ClientController> logger,
            IClientService clientService)
        {
            _logger = logger;
            _clientService = clientService; 
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> GetClient([FromQuery] string docto)
        {
            var result = await _clientService.GetByDocto(docto);

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
            var result = await _clientService.GetAll();

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



        [HttpPost(Name = "PostClient")]
        public async Task<IActionResult> RegisterClient(RegisterClientDto registerClient) 
        {
             var result = await _clientService.Register(registerClient);

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
