using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.Filters;
using TicketTracking.Domain.Exceptions;

namespace TicketTracking.Api.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    private readonly IServiceProvider _serviceProvider;

    public ValidationFilter(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        foreach (var argument in context.ActionArguments)
        {
            var argumentType = argument.Value?.GetType();
            if (argumentType == null) continue;

            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);
            if (_serviceProvider.GetService(validatorType) is not IValidator validator) continue;

            var validateMethod = validator.GetType().GetMethod("ValidateAsync", new[] { argumentType, typeof(CancellationToken) });
            if (validateMethod == null) continue;

            if (validateMethod.Invoke(
                validator,
                [argument.Value, CancellationToken.None]
            ) is not Task<ValidationResult> validationTask) return;

            var validationResult = await validationTask;

            if (!validationResult.IsValid)
            {
                throw new CustomValidationException(validationResult.ToDictionary());
            }
        }

        await next();
    }
}
