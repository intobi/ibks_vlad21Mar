using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicketTracking.Domain.Exceptions;

namespace TicketTracking.Api.Handlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = HandleException(exception);

        httpContext.Response.StatusCode = problemDetails.Status ?? StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private ProblemDetails HandleException(Exception exception)
    {
        switch (exception)
        {
            case CustomValidationException validationException:
                var details = CreateProblemDetails(
                    StatusCodes.Status400BadRequest, "Validation error occured", exception.Message);
                details.Extensions["errors"] = validationException.Errors;
                return details;

            case NotFoundException:
                return CreateProblemDetails(
                    StatusCodes.Status404NotFound, "The requested resource was not found", exception.Message);

            default:
                _logger.LogError(exception, "An unhandled exception occurred");
                return CreateProblemDetails(
                    StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred");
        }
    }

    private static ProblemDetails CreateProblemDetails(int status, string title, string detail)
    {
        return new ProblemDetails 
        {
            Status = status,
            Title = title,
            Detail = detail
        };
    }
}
