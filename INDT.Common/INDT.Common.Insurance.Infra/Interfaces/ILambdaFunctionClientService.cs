using INDT.Common.Insurance.Dto;

namespace INDT.Common.Insurance.Infra.Interfaces.AWS
{
    public interface ILambdaFunctionClientService
    {
        Task InvokeLambdaAsync(RequestLambdaTestDto request);
    }
}
