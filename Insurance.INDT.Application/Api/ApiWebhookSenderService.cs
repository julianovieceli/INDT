using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Request;
using INDT.Common.Insurance.Dto.Response;
using Insurance.INDT.Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Personal.Common.Domain;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Insurance.INDT.Application.Api
{
    public class ApiWebhookSenderService: IApiWebhookSenderService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly WebhookSettings _webhookSettings;
        private readonly ILogger<ApiWebhookSenderService> _logger;

        public ApiWebhookSenderService(IHttpClientFactory httpClientFactory,
        IOptions<WebhookSettings> webhookSettings,
        ILogger<ApiWebhookSenderService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _webhookSettings = webhookSettings.Value;
            _logger = logger;
        }

        public async Task<Result> SendWebhook(WebhookDto webhookDto)
        {

            try
            {
                var httpClient = _httpClientFactory.CreateClient(nameof(HttpClientEnum.API_WEBHOOK));

                var serviceParams = JsonSerializer.Serialize(webhookDto);
                string servicePath = _webhookSettings.Url;
                using HttpContent serviceRequestContent = new StringContent(serviceParams, Encoding.UTF8, new MediaTypeHeaderValue("application/json"));


                var response = await httpClient.PostAsync(servicePath, serviceRequestContent);

                return await HandleGetApprovalResponseAsync(response);
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                throw;
            }
        }

        private async Task<Result> HandleGetApprovalResponseAsync(HttpResponseMessage response)
        {
            try
            {
                var responseAsString = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Sucesso ao enviar webhook");
                    return Result.Success;
                }
                else
                if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    _logger.LogError("404 Webhook não encontrado");
                    return Result.Failure("404", "Servico de webhook não encontrado");
                }
                else
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    _logger.LogError("403 Forbiden");
                    return Result.Failure("403", "Forbiden");
                }
                else

                {
                    if (string.IsNullOrWhiteSpace(responseAsString))
                        throw new Exception("No content response");

                    var result = JsonSerializer.Deserialize<Result?>(responseAsString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    return Result.Failure(result.ErrorCode, result.ErrorMessage);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Erro ao processar a resposta da API de webhook");
                // Log exception here if needed
                throw;
            }
        }
    }
}
