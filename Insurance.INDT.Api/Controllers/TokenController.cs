using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Personal.Common.Domain.Interfaces.Services;
using static BasicAuthenticationHandler;

namespace Insurance.INDT.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    
    public class TokenController : Controller
    {
        private readonly ILogger<TokenController> _logger;
        private readonly ITokenService _tokenService;

        public TokenController(ILogger<TokenController> logger, ITokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService;
        }


        [HttpGet("")]
        [Authorize(AuthenticationSchemes = BasicAuthenticationOptions.DefaultScheme)]
        public IActionResult GenerateToken([FromQuery] bool admin)
        {
            var username = User.Identity?.Name;


            var result = _tokenService.GenerateToken(username);

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);
            }
            _logger.LogInformation("Ok");
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet("validate-token-admin")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles ="Admin")]
        public IActionResult ValidateToken()
        {
            _logger.LogInformation("Ok");
            return Ok();
        }

        [HttpGet("validate-token-guest")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Guest")]
        public IActionResult ValidateTokenGest()
        {
            _logger.LogInformation("Ok");
            return Ok();
        }


    }
}
