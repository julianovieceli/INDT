// Example Lambda function handler
using Amazon.Lambda.Core;
using INDT.Common.Insurance.Dto;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Servless;
public class AwsLambdaFunction
{
    public string FunctionHandler(RequestLambdaTestDto input, ILambdaContext context)
    {
        context.Logger.LogInformation($"Received input: {input.Nome}");
        return $"Hello, {input.Nome}!";
    }
}