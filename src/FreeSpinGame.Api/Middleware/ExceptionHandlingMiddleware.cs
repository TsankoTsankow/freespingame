using System.Net;
using System.Text.Json;
using FreeSpinGame.Domain.Exceptions;

namespace FreeSpinGame.Api.Middleware;

public class ExceptionHandlingMiddleware (RequestDelegate next) 
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = context.Response;
        var errorResponse = new {error = exception.Message};

        switch (exception)
        {
            case SpinLimitReachedException:
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                break;
            case EntityNotFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse = new  {error = "An internal error occurred"};
                break;
        }
        
        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }
}