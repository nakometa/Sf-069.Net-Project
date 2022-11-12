using SportsHub.Domain.Models.Constants;

namespace SportsHub.Api.Exceptions.CustomExceptionModels;

public class NotFoundException: BaseCustomException
{
    public NotFoundException(string message)
    {
        ErrorCode = 404;
        Message = message;
    }
}