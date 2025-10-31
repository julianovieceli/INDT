// Example Lambda function handler
using Amazon.Lambda.Core;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Servless;
public class AwsLambdaFunction
{
    public string FunctionHandler(string input, ILambdaContext context)
    {
        context.Logger.LogInformation($"Received input: {input}");
        return $"Hello, {input}!";
    }
}