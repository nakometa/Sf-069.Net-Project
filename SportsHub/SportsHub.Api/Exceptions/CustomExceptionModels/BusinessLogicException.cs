using System.Text.Json;

namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class BusinessLogicException: Exception
{
    public BusinessLogicException()
    {
    }

    public int StatusCode { get; } = 400;
    public string Message { get; set; }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}