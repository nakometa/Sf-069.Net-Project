using System.Net;
using SportsHub.Api.Exceptions.CustomExceptionModels;

namespace SportsHub.Api.Exceptions;

public class ExceptionHandler: IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (BusinessLogicException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (NotFoundException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        string result = null;
        context.Response.ContentType = "application/json";
        
        switch (ex)
        {
            case BusinessLogicException:
            {
                //Maybe we need interface ICustomException so we can use it as a dependacy and create the corespond exception this way 
                //not with a var ex = New Exception();
                var exception = new BusinessLogicException()
                {
                    //Maybe we can have constants file with this messages and set them in constructor
                    Message = "There has been error in you business logic"
                };
                context.Response.StatusCode = exception.StatusCode;
                result = exception.ToString();
                break;
            }
            case NotFoundException:
            {
                //Maybe we need interface ICustomException so we can use it as a dependacy and create the corespond exception this way 
                //not with a var ex = New Exception();
                var exception = new NotFoundException()
                {
                    Message = "The item you need does not exist"
                };
                context.Response.StatusCode = exception.StatusCode;
                result = exception.ToString();
                break;
            }
            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(ex.Message);
                break;
        }

        await context.Response.WriteAsync(result);
    }
}