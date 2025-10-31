using Amazon;
using Amazon.Lambda;
using Amazon.Lambda.Model;
using Amazon.Runtime;
using INDT.Common.Insurance.Dto;
using INDT.Common.Insurance.Infra.Interfaces.AWS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using System.Text.Json;

namespace Insurance.INDT.Application.Servless.AWS
{
    public  class AWSLambdaFunctionClientService : IAWSLambdaFunctionClientService
    {
        private const string FunctionName = "AWSLambdaTest";
        private const string AwsRegion = "us-east-1";
        public async Task InvokeLambdaAsync(RequestLambdaTestDto request)
        {
            // 1. Create the AWS Lambda Client

            AWSCredentials credentials = new BasicAWSCredentials("teste", "teste");

            var config = new AmazonLambdaConfig
            {
                DefaultAWSCredentials = credentials,
                ServiceURL = "http://localhost:4566" // Or your specific LocalStack endpoint
            };

            // Ensure you have configured AWS credentials (e.g., environment variables, profile, etc.)
            using var client = new AmazonLambdaClient(config);


            // 2. Prepare the Input Payload
            // The payload must be a JSON string that the target Lambda function expects.
            string payloadJson = JsonSerializer.Serialize(request);

            // Convert the JSON string to a MemoryStream
            var payloadStream = new MemoryStream(Encoding.UTF8.GetBytes(payloadJson));

            // 3. Create the Invoke Request
            var invokeRequest = new InvokeRequest
            {
                FunctionName = FunctionName,
                InvocationType = InvocationType.RequestResponse, // Wait for the response
                PayloadStream = payloadStream
            };

            Console.WriteLine($"Invoking Lambda function: {FunctionName}...");

            try
            {
                // 4. Invoke the Function
                var response = await client.InvokeAsync(invokeRequest);

                // 5. Process the Response
                string responseString;
                using (var sr = new StreamReader(response.Payload))
                {
                    responseString = await sr.ReadToEndAsync();
                }

                // Check for Lambda function errors (different from invocation errors)
                if (!string.IsNullOrEmpty(response.FunctionError))
                {
                    Console.WriteLine($"\n❌ Lambda Function Error: {response.FunctionError}");
                    Console.WriteLine($"Error Details: {responseString}");
                }
                else
                {
                    Console.WriteLine($"\n✅ Lambda Invocation Successful! Status Code: {response.StatusCode}");
                    Console.WriteLine($"Returned Payload: {responseString}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n🚨 Invocation Failed: {ex.Message}");
            }
        }
    }

    public static class IoC
    {
        public static IServiceCollection AddAWSLambdaClientService(this IServiceCollection services)
        {
            services.AddScoped<IAWSLambdaFunctionClientService, AWSLambdaFunctionClientService>();
            return services;
        }
    }
}
