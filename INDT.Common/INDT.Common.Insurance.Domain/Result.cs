using System.Net;

namespace INDT.Common.Insurance.Domain;

public class Result
{
    protected Result() { }

    protected Result(string errorCode, string? errorMessage, HttpStatusCode statusCode)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        IsFailure = true;
        StatusCode = (int)statusCode;
    }

    protected Result(string errorCode, string? errorMessage, int statusCode)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        IsFailure = true;
        StatusCode = statusCode;
    }

    public string ErrorCode { get; set; }
    public string? ErrorMessage { get; set; }
    public int StatusCode { get; set; }
    public bool IsFailure { get; init; }
    
    public static Result Success => new();
    public static Result Failure(string errorCode, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new(errorCode, null, statusCode);

    public static Result Failure(string errorCode, string errorMessage, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        => new(errorCode, errorMessage, statusCode);


}

public class Result<T> : Result
{
    public T Value { get; set; }

    protected Result(T value, HttpStatusCode statusCode = HttpStatusCode.OK) 
    {
        this.Value = value;
        this.StatusCode = (int)statusCode;
    }

    public static Result<T> Success(T value) => new(value);
}


