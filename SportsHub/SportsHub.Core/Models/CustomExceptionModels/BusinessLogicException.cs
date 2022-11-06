namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class BusinessLogicException: Exception
{
    public BusinessLogicException(string message)
    {
        Message = message;
    }

    public BusinessLogicException(int code, string message): base()
    {
        ErrorCode = code;
        Message = message;
    }
    
    public int ErrorCode { get; set; } 
    public string Message { get; }

    public override string ToString()
    {
       return $"{ErrorCode}-BusinessLogicException: {Message}";
    }
}