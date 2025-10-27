using INDT.Common.Insurance.Infra.Interfaces.AWS;
using INDT.Common.Insurance.Infra.Interfaces.Azure;
using Microsoft.AspNetCore.Mvc;

namespace Insurance.INDT.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class StorageController : Controller
    {
        private readonly ILogger<StorageController> _logger;

        
        private readonly IAWSStorageService _AWSStorageService;
        private readonly IAzureStorageService _azureStorageService;

        public StorageController(ILogger<StorageController> logger,
            IAWSStorageService AWSStorageService,
            IAzureStorageService azureStorageService)
        {
            _logger = logger;
            _AWSStorageService = AWSStorageService;
            _azureStorageService = azureStorageService;
        }

        [HttpPost("upload/s3")]
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

        [HttpPost("upload/azure")]
        // The [FromForm] attribute is crucial for multipart/form-data binding
        public async Task<IActionResult> UploadFileToAzure([FromForm] IFormFile file)
        {

            var result = await _azureStorageService.UploadFile(file);

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);
            }
            _logger.LogInformation("Ok");
            return Ok(result);


        }

        [HttpGet("download/azure")]
        // The [FromForm] attribute is crucial for multipart/form-data binding
        public async Task<IActionResult> DownloadFileFromAzure([FromQuery] string fileName)
        {

            var result = await _azureStorageService.DownloadFile(fileName);

            if (result.IsFailure)
            {
                _logger.LogError($"Error{result.ErrorCode}");
                return BadRequest(result);
            }
            _logger.LogInformation("Ok");
            return Ok();

        }
    }
}
