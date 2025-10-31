--Listar functions
awslocal lambda list-functions


-- Cria lambda arquivo zipado
dotnet lambda package --configuration Release --output-package function.zip

-- Delete lambda-function
awslocal lambda delete-function --function-name AWSLambdaTest



--sobe AWS
awslocal lambda create-function \
    --function-name AWSLambdaTest \
    --zip-file fileb://function.zip \
    --handler Insurance.INDT.Servless.LambdaFunction::Servless.AwsLambdaFunction::FunctionHandler \
    --runtime dotnet8 \
    --role arn:aws:iam::000000000000:role/lambda-role # Use a dummy role for LocalStack

	
--teste chamada	
	awslocal lambda invoke --function-name AWSLambdaTest --payload '{\"Nome\": \"teste\"}' --cli-binary-format raw-in-base64-out  output.json
	
	awslocal lambda invoke --function-name AWSLambdaTest --payload file://payload.json --cli-binary-format raw-in-base64-out  output.json
	
	
	
    awslocal lambda invoke --function-name AWSLambdaTest --payload "{\"input\": \"teste\"}" --cli-binary-format raw-in-base64-out  output.json
	
  