using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Response;
using Insurance.ProposalHire.INDT.Application.Api;
using Insurance.ProposalHire.INDT.Application.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Personal.Common.Domain;
using System.Net;
using System.Text.Json;

namespace Insurance.ProposalHire.INDT.Application;

public class ApiProposalService : IApiProposalService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ProposalUrlSettings _proposalUrlSettings;
    private readonly ILogger<ApiProposalService> _logger;

    public ApiProposalService(
        IHttpClientFactory httpClientFactory,
        IOptions<ProposalUrlSettings> proposalUrlSettings,
        ILogger<ApiProposalService> logger
    )
    {
        _httpClientFactory = httpClientFactory;
        _proposalUrlSettings = proposalUrlSettings.Value;
        _logger = logger;
    }

    public async Task<Result> GetProposalById(int id)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient(nameof(HttpClientEnum.API_APPROVAL));
            var serviceParams = JsonSerializer.Serialize(new { Id = id });
            
            string url = _proposalUrlSettings.ProposalUrl + $"?id={id}";

            var response = await httpClient.GetAsync(url);

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
                _logger.LogInformation("Sucesso ao coletar endpoint de proposta");
                ProposalDto? proposalDto = JsonSerializer.Deserialize<ProposalDto?>(responseAsString, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return Result<ProposalDto>.Success(proposalDto);
            }
            else
            if(response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogError("404 Proposta não encontrada");
                return Result.Failure("404", "Servico de propotsa não encontrado");
            }
            else
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
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
        catch(Exception e)
        {
            _logger.LogError(e, "Erro ao processar a resposta da API de Propostas");
            // Log exception here if needed
            throw;
        }
    }
}
