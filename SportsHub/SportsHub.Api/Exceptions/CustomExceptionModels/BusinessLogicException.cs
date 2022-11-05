using System.Text.Json;

namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class BusinessLogicException: CustomException
{
    public BusinessLogicException(string message)
    {
        Message = message;
    }

    public int StatusCode { get; } = 400; 
    public string Message { get; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}