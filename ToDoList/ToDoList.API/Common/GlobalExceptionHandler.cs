using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToDoListApp.API.Extensions;

namespace ToDoListApp.API.Common;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(
            exception, "Exception occurred: {Message}", exception.Message);

        var statusCode = exception.ParseException();

        httpContext.Response.StatusCode = (int)statusCode;

        var response = new
        {
            status = httpContext.Response.StatusCode,
            message = exception.Message
        };

        await httpContext.Response.WriteAsJsonAsync(response);

        return true;
    }
}
