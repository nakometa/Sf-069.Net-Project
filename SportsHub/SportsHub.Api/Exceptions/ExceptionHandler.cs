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
            await HandleBusinessLogicExceptionAsync(context, ex);
        }
        catch (NotFoundException ex)
        {
            await HandleNotFoundExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(ex.Message);
        }
    }

    private async Task HandleNotFoundExceptionAsync(HttpContext context, NotFoundException ex)
    {
        context.Response.ContentType = "/application/json";
        context.Response.StatusCode = ex.ErrorCode;
        string result = ex.ToString();
        await context.Response.WriteAsync(result);
    }

    private async Task HandleBusinessLogicExceptionAsync(HttpContext context, BusinessLogicException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex.ErrorCode;
        string result = ex.ToString();
        await context.Response.WriteAsync(result);
    }
}