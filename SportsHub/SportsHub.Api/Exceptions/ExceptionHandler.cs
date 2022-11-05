using System.Net;
using SportsHub.Api.Exceptions.CustomExceptionModels;

namespace SportsHub.Api.Exceptions;

public class ExceptionHandler: IMiddleware
{
    private CustomException _exception;
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
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
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
                _exception = new BusinessLogicException("There has been error in you business logic");
                context.Response.StatusCode = _exception.StatusCode;
                result = _exception.ToString();
                break;
            }
            case NotFoundException:
            {
                _exception = new NotFoundException("The item you need does not exist");
                context.Response.StatusCode = _exception.StatusCode;
                result = _exception.ToString();
                break;
            }
        }

        await context.Response.WriteAsync(result);
    }
}