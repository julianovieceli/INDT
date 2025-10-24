using Amazon.S3;
using Amazon.S3.Model;
using Amazon.SQS;
using Azure.Core;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Response;
using Insurance.INDT.Application.ServiceBus.AWS;
using Insurance.INDT.Application.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Insurance.INDT.Application.ServiceBus.AWS
{
    public class AWSStorageService : IAWSStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName = "indt-test-files";
        private readonly ILogger<AWSStorageService> _logger;
        private readonly AmazonLocalS3Config _amazonS3Config;

        public AWSStorageService(ILogger<AWSStorageService> logger, IOptions<AmazonLocalS3Config> amazonS3Config, IAmazonS3 s3Client)
        {
            _logger = logger;
            _s3Client = s3Client;
            _amazonS3Config = amazonS3Config.Value;
        }

        public async Task<Result> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                _logger.LogError("No file uploaded or file is empty.");
                return Result.Failure("400", "No file uploaded or file is empty.");
            }

            try
            {
                PutObjectResponse response = null;
                using (var stream = file.OpenReadStream())
                {
                    var putObjectRequest = new PutObjectRequest
                    {
                        BucketName = _amazonS3Config.BucketName,
                        Key = file.FileName, // Use the original file name as the S3 key
                        InputStream = stream,
                        ContentType = file.ContentType
                    };

                    response = await _s3Client.PutObjectAsync(putObjectRequest);

                    
                }

                string url = _s3Client.Config.ServiceURL + _amazonS3Config.BucketName + "/" + file.FileName;

                UploadedFileResponseDto uploadedFileResponseDto = new UploadedFileResponseDto()
                {
                    Url = url
                };


                string successMsg = $"File '{file.FileName}' uploaded successfully to S3. HttpStatusCode: {response.HttpStatusCode}";
                _logger. LogInformation(successMsg);

                return Result<UploadedFileResponseDto>.Success(uploadedFileResponseDto);
            }
            catch (AmazonS3Exception s3Ex)
            {
                string s3Error = $"S3 error: {s3Ex.Message}";
                _logger.LogError(s3Error);
                return Result.Failure("500", s3Error);
            }
            catch (System.Exception ex)
            {
                string internalServerError = $"S3 error: {ex.Message}";
                _logger.LogError(internalServerError);
                return Result.Failure("500", internalServerError);
            }

        }
    }


}

public static class IoC
{
    public static IServiceCollection AddAWSStorageService(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddOptions<AmazonLocalS3Config>().BindConfiguration(nameof(AmazonLocalS3Config));

        services.AddAWSService<IAmazonS3>();
        services.AddDefaultAWSOptions(configuration.GetAWSOptions());
        return services.AddScoped<IAWSStorageService, AWSStorageService>();

    }
}
