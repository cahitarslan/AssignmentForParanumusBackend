using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;
using WebAPI.Models;

namespace WebAPI.CustomMiddlewares;

public class GlobalExceptionHandler : IExceptionHandler
{
    //private readonly ILogger _logger;
    //public GlobalExceptionHandler(ILogger logger) => _logger = logger;
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";

        //_logger.LogError($"Something went wrong: : {exception}");

        var message = exception switch
        {
            AccessViolationException => "Access violation error from the GlobalExceptionHandler",
            WebException => "An error occurred while accessing the network",
            //...
            //...
            //...
            _ => "Internal Server Error"
        };

        Log.Error(exception, message);

        await httpContext.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = httpContext.Response.StatusCode,
            Message = message
        }.ToString());

        return true;
    }
}
