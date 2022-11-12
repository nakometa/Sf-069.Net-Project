namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class BusinessLogicException: BaseCustomException 
{
    public BusinessLogicException(int code, string message)
    {
        ErrorCode = code;
        Message = message;
    }
}