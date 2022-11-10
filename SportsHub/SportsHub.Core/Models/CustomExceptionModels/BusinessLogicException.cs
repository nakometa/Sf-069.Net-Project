namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class BusinessLogicException: BaseCustomException 
{
    public BusinessLogicException(string message)
    {
        Message = message;
    }

    public BusinessLogicException(int code, string message)
    {
        ErrorCode = code;
        Message = message;
    }
}