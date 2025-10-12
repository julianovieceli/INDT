using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Response;
using Insurance.ProposalHire.INDT.Application.Api;
using Insurance.ProposalHire.INDT.Application.Settings;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Insurance.ProposalHire.INDT.Application;

public class ApiProposalService : IApiProposalService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ProposalUrlSettings _proposalUrlSettings;

    public ApiProposalService(
        IHttpClientFactory httpClientFactory,
        IOptions<ProposalUrlSettings> proposalUrlSettings
    )
    {
        _httpClientFactory = httpClientFactory;
        _proposalUrlSettings = proposalUrlSettings.Value;
    }

    public async Task<Result> GetProposalById(int id)
    {
        try
        {
            var httpClient = _httpClientFactory.CreateClient(nameof(HttpClientEnum.API_APPROVAL));
            var serviceParams = JsonSerializer.Serialize(new { Id = id });
            
            string url = _proposalUrlSettings.ProposalUrl + $"?id={id}";

            using HttpContent serviceRequestContent = new StringContent(serviceParams, Encoding.UTF8, "application/json");
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
            ProposalDto? proposalDto = JsonSerializer.Deserialize<ProposalDto?>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (response.IsSuccessStatusCode && proposalDto != null)
            {
                return Result<ProposalDto>.Success(proposalDto);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(responseAsString))
                    throw new Exception("No content response");

                return Result.Failure("999");
            }
        }
        catch
        {
            // Log exception here if needed
            throw;
        }
    }
}
