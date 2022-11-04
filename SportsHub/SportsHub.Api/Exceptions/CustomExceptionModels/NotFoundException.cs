using System.Text.Json;

namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class NotFoundException: Exception
{
    public NotFoundException()
    {
    }

    public int StatusCode { get; } = 404;
    public string Message { get; set; }
    
    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}