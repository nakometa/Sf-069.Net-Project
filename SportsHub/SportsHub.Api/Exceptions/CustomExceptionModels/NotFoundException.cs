using System.Text.Json;

namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class NotFoundException: CustomException
{
    public NotFoundException(string message)
    {
        Message = message;
    }



    public int StatusCode { get; } = 404;
    public string Message { get; set; }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}