using INDT.Common.Insurance.Domain;
using INDT.Common.Insurance.Dto.Request;

namespace Insurance.INDT.Application.Api
{
    public interface IApiWebhookSenderService
    {
        Task<Result> SendWebhook(WebhookDto webhookDto);
    }
}
