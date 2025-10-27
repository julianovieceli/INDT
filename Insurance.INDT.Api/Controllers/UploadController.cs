using INDT.Common.Insurance.Infra.Interfaces.AWS;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.INDT.Api.Controllers
{
    public class UploadController : Controller
    {
        private readonly ILogger<UploadController> _logger;

        
        private readonly IAWSStorageService _AWSStorageService;

        public UploadController(ILogger<UploadController> logger,
            IAWSStorageService AWSStorageService)
        {
            _logger = logger;
            _AWSStorageService = AWSStorageService;
        }

        [HttpPost("upload-s3")]
        // The [FromForm] attribute is crucial for multipart/form-data binding
        public async Task<IActionResult> UploadFileToS3([FromForm] IFormFile file)
        {

            var result = await _AWSStorageService.UploadFile(file);

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);
            }
            _logger.LogInformation("Ok");
            return Ok(result);


        }
    }
}
