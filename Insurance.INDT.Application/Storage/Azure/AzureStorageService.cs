using Azure;
using Azure.Storage.Blobs;
using INDT.Common.Insurance.Dto.Response;
using INDT.Common.Insurance.Infra.Interfaces.Azure;
using Insurance.INDT.Application.Settings;
using Insurance.INDT.Application.Storage.AWS;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Personal.Common.Domain;
using System.Net;

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
            
            if (file == null || file.Length == 0)
            {
                _logger.LogError("No file uploaded or file is empty.");
                return Result.Failure("400", "No file uploaded or file is empty.");
            }

            string url = string.Empty;
            int httpStatusCodeResponse = (int)HttpStatusCode.BadRequest;

            try
            {
                using (var stream = file.OpenReadStream())
                {
                    var containerClient = _blobServiceClient.GetBlobContainerClient(_azureLocalBlobStorageConfig.ContainerName);
                    await containerClient.CreateIfNotExistsAsync();
                    var blobClient = containerClient.GetBlobClient(file.FileName);
                    var response = await blobClient.UploadAsync(stream, overwrite: true);
                    url = blobClient.Uri.ToString();

                    httpStatusCodeResponse = response.GetRawResponse().Status;
                }

                UploadedFileResponseDto uploadedFileResponseDto = new UploadedFileResponseDto()
                {
                    Url = url
                };


                string successMsg = $"File '{file.FileName}' uploaded successfully to S3. HttpStatusCode: {httpStatusCodeResponse}";
                _logger.LogInformation(successMsg);

                return Result<UploadedFileResponseDto>.Success(uploadedFileResponseDto);
            }
            catch (Exception ex)
            {
                string internalServerError = $"S3 error: {ex.Message}";
                _logger.LogError(internalServerError);
                return Result.Failure("500", internalServerError);
            }

        }

        public async Task<Result> DownloadFile(string fileName)
        {
            ArgumentNullException.ThrowIfNullOrWhiteSpace(fileName, "fileName");

            
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(_azureLocalBlobStorageConfig.ContainerName);
                if (await containerClient.ExistsAsync())
                {
                    var blobClient = containerClient.GetBlobClient(fileName);
                    var response = await blobClient.DownloadToAsync($"C:\\{fileName}");
                }
                else
                    return Result.Failure("400", $"Container not found: {_azureLocalBlobStorageConfig.ContainerName}");

                return Result.Success;
            }
            catch(RequestFailedException azEx)
            {
                string s3Error = $"Azure Blob error: {azEx.Message}";
                _logger.LogError(s3Error);

                if (azEx.Status == 404)
                    return Result.Failure("404", $"File not found: {fileName}");

                return Result.Failure("400", s3Error);
            }

            catch (Exception ex)
            {
                string internalServerError = $"S3 error: {ex.Message}";
                _logger.LogError(internalServerError);
                return Result.Failure("500",internalServerError);
            }

        }
    }

    public static class IoC
    {
        public static IServiceCollection AddAzureStorageService(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddOptions<AzureBlobStorageConfig>().BindConfiguration(nameof(AzureBlobStorageConfig));

            var azureBlobStorageConnString = configuration.GetSection("AzureBlobStorageConfig:ConnectionString");

            
            services.AddSingleton(new BlobServiceClient(azureBlobStorageConnString.Value));
            return services.AddScoped<IAzureStorageService, AzureStorageService>();
        }
    }


}


