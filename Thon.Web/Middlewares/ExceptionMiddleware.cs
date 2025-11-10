using System.Text;
using Thon.App.Exceptions;
using Thon.Web.Exceptions;

namespace Thon.Web.Middlewares;

public class ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment environment, ILogger<ExceptionMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly IWebHostEnvironment _environment = environment;

    public async Task InvokeAsync(HttpContext context)
    {
        try { await _next(context); }
        catch (Exception ex)
        {
            HandleException(context, ex);

            if (_environment.IsDevelopment())
                await WriteExceptionToResponse(context, ex);
        }
    }

    private async Task WriteExceptionToResponse(HttpContext context, Exception exception)
    {
        var errorMessage = $"{exception.Message}\n\n{exception.StackTrace}";
        var errorMessageBytes = Encoding.UTF8.GetBytes(errorMessage);

        context.Response.ContentType = "text/plain";
        await context.Response.Body.WriteAsync(errorMessageBytes);
    }

    private void HandleException(HttpContext context, Exception exception)
    {
        if (exception is ArgumentException || exception is ThonArgumentException || exception is ThonApiBadRequestException)
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

        else if (exception is ThonApiUnauthorizedException)
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;

        else if (exception is ThonApiForbiddenException)
            context.Response.StatusCode = StatusCodes.Status403Forbidden;

        else if (exception is ThonApiNotFoundException)
            context.Response.StatusCode = StatusCodes.Status404NotFound;

        else if (exception is ThonConflictException || exception is ThonApiConflictException)
            context.Response.StatusCode = StatusCodes.Status409Conflict;

        else
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            logger.LogError(exception: exception, message: "500 ERROR");
        }
    }
}
