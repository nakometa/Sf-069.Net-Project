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
        catch (Exception ex) when (ex is BaseCustomException) 
        {
            await HandleCustomExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
    }

    private async Task HandleCustomExceptionAsync(HttpContext context, Exception ex)
    {
        if (ex is NotFoundException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
        else if (ex is BusinessLogicException)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        }
        context.Response.ContentType = "/application/json";
        string result = ex.ToString();
        await context.Response.WriteAsync(result);
    }
}