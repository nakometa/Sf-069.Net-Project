namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class NotFoundException: BaseCustomException
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
}