using Amazon.S3;
using Amazon.S3.Model;
using Azure.Storage.Blobs;
using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Response;
using INDT.Common.Insurance.Infra.Interfaces.Azure;
using Insurance.INDT.Application.Settings;
using Insurance.INDT.Application.Storage.AWS;
using Insurance.INDT.Application.Storage.Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Insurance.INDT.Application.Storage.Azure
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<AWSStorageService> _logger;
        private readonly AzureBlobStorageConfig _azureLocalBlobStorageConfig;

        public AzureStorageService(ILogger<AWSStorageService> logger,
            IOptions<AzureBlobStorageConfig> azureLocalBlobStorageConfig, BlobServiceClient blobServiceClient)
        {
            _logger = logger;
            _blobServiceClient = blobServiceClient;
            _azureLocalBlobStorageConfig = azureLocalBlobStorageConfig.Value;
        }

        public async Task<Result> UploadFile(IFormFile file)
        {
            return Task.FromResult(Result.Success).Result;

            if (file == null || file.Length == 0)
            {
                _logger.LogError("No file uploaded or file is empty.");
                return Result.Failure("400", "No file uploaded or file is empty.");
            }

            //try
            //{
            //    PutObjectResponse response = null;
            //    using (var stream = file.OpenReadStream())
            //    {
            //        var putObjectRequest = new PutObjectRequest
            //        {
            //            BucketName = _amazonS3Config.BucketName,
            //            Key = file.FileName, // Use the original file name as the S3 key
            //            InputStream = stream,
            //            ContentType = file.ContentType
            //        };

            //        response = await _s3Client.PutObjectAsync(putObjectRequest);
            //    }

            //    //TimeSpan duration = TimeSpan.FromMinutes(5); // URL valid for 5 minutes

            //    //var request = new GetPreSignedUrlRequest
            //    //{
            //    //    BucketName = _amazonS3Config.BucketName,
            //    //    Key = file.FileName,
            //    //    Expires = DateTime.UtcNow.Add(duration),
            //    //    Protocol = Protocol.HTTP, // Or Protocol.HTTP if needed
            //    //    Verb = HttpVerb.GET // The operation the URL allows
            //    //};

            //    //string url = _s3Client.GetPreSignedURL(request);

            //    string url = _amazonS3Config.BaaseUrlToGetFile + _amazonS3Config.BucketName + "/" + file.FileName;

            //    UploadedFileResponseDto uploadedFileResponseDto = new UploadedFileResponseDto()
            //    {
            //        Url = url
            //    };


            //    string successMsg = $"File '{file.FileName}' uploaded successfully to S3. HttpStatusCode: {response.HttpStatusCode}";
            //    _logger.LogInformation(successMsg);

            //    return Result<UploadedFileResponseDto>.Success(uploadedFileResponseDto);
            //}
            //catch (AmazonS3Exception s3Ex)
            //{
            //    string s3Error = $"S3 error: {s3Ex.Message}";
            //    _logger.LogError(s3Error);
            //    return Result.Failure("500", s3Error);
            //}
            //catch (Exception ex)
            //{
            //    string internalServerError = $"S3 error: {ex.Message}";
            //    _logger.LogError(internalServerError);
            //    return Result.Failure("500", internalServerError);
            //}

        }
    }

    public static class IoC
    {
        public static IServiceCollection AddAzureStorageService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddOptions<AzureBlobStorageConfig>().BindConfiguration(nameof(AzureBlobStorageConfig));

            var connectionString = configuration.GetConnectionString("AzureBlobStorage:ConnectionString");
            services.AddSingleton(new BlobServiceClient(connectionString));
            return services.AddScoped<IAzureStorageService, AzureStorageService>();
        }
    }


}


