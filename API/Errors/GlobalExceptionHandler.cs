// Errors/GlobalExceptionHandler.cs
using Microsoft.AspNetCore.Diagnostics;

namespace API.Errors;

internal sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IHostEnvironment env,
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "{message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        // In development, include the stack trace in the details
        var detail = env.IsDevelopment()
            ? exception.StackTrace
            : "Internal Server Error";

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails =
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = exception.Message,
                Detail = detail
            }
        });
    }
}