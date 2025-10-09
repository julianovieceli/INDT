using Insurance.INDT.Application.Services.Interfaces;
using Insurance.INDT.Dto.Request;
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
            return Ok(result);
        }
    }
}
