using INDT.Common.Insurance.Dto.Request;
using Personal.Common.Domain;

namespace Insurance.INDT.Application.Api
{
    public interface IApiWebhookSenderService
    {
        Task<Result> SendWebhook(WebhookDto webhookDto);
    }
}
