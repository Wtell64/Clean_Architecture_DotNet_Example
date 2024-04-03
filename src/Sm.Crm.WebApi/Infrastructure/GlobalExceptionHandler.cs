using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Serilog;
using Sm.Crm.Application.Common.Exceptions;
using Sm.Crm.Application.Common.Interfaces;
using ILogger = Serilog.ILogger;

namespace Sm.Crm.WebApi.Infrastructure;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger _logger;
    private readonly IConfiguration _configuration;

    public GlobalExceptionHandler(IAppLogger logger, IConfiguration configuration)
    {
        _logger = logger.CreateMongoLogger();
        _configuration = configuration;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception ex, CancellationToken cancellationToken)
    {
        var exceptionMessage = ex.Message;
        var exceptionType = ex.GetType();

        if (exceptionType == typeof(ValidationException))
        {
            var exception = (ValidationException)ex;
            httpContext.Response.StatusCode = (int)StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(new ValidationProblemDetails(exception.Errors)
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Validation Error",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            });

            _logger.Error($"[VALIDATIN ERROR] Type: {exceptionType}, Message: {exceptionMessage}, Time: {DateTime.UtcNow}");

            // ASP.NET'in standart DevelopeExceptionPage ya da ExceptionHandler'ını kullanmaz
            return true;
        }

        else if (exceptionType == typeof(NotFoundException))
        {
            var exception = (NotFoundException)ex;
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = exception.Message
            });

            _logger.Error($"[NOT FOUND] Type: {exceptionType}, Message: {exceptionMessage}, Time: {DateTime.UtcNow}");

            return true;
        }

        else if (exceptionType == typeof(UnauthorizedAccessException))
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            });

            _logger.Error($"[UNAUTHORIZED] Type: {exceptionType}, Message: {exceptionMessage}, Time: {DateTime.UtcNow}");

            return true;
        }

        else if (exceptionType == typeof(ForbiddenAccessException))
        {
            httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            });

            var msg = $"[FORBIDDEN] Type: {exceptionType}, Message: {exceptionMessage}, Time: {DateTime.UtcNow}";
            _logger.Error(msg);

            return true;
        }

        else if (exceptionType == typeof(SqlException))
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Db Error",
                Detail = ex.Message,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            });

            var msg = $"[DB ERROR] Type: {exceptionType}, Message: {exceptionMessage}, Time: {DateTime.UtcNow}";
            Log.Error(msg);
            _logger.Error(msg);

            return true;
        }

        else
        {
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Error",
                Detail = ex.Message
            });

            _logger.Error($"[ERROR] Type: {exceptionType}, Message: {exceptionMessage}, Time: {DateTime.UtcNow}");

            // Return false to continue with the default behavior
            // - or - return true to signal that this exception is handled
            return false;
        }
    }
}
