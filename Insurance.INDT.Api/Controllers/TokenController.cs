using INDT.Common.Insurance.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        [Authorize()]

        public IActionResult GenerateToken()
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
    }
}
