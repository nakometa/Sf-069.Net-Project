namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class BaseCustomException: Exception
{
    public int ErrorCode { get; set; } 
    public string Message { get; set; }
    
    public override string ToString()
    {
        return $"{ErrorCode}-{GetType().Name}: {Message}";
    }
}