namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class NotFoundException: Exception
{
    public NotFoundException(string message)
    {
        Message = message;
    }

    public NotFoundException(int code, string message)
    {
        ErrorCode = code;
        Message = message;
    }

    public int ErrorCode { get; set; } 
    public string Message { get; }

    public override string ToString()
    {
        return $"{ErrorCode}-NotFoundException: {Message}";
    }
}