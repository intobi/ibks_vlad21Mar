using FluentValidation;
using TicketTracking.Domain.Dto;

namespace TicketTracking.Core.Validators;
public class TicketRequestDtoValidator : AbstractValidator<TicketRequestDto>
{
    public TicketRequestDtoValidator()
    {
        RuleFor(request => request.StatusId)
            .GreaterThan(0);

        RuleFor(request => request.TicketTypeId)
            .GreaterThan(0);

        RuleFor(request => request.PriorityId)
            .GreaterThan(0);

        RuleFor(request => request.Title)
            .MaximumLength(250);

        RuleFor(request => request.ApplicationName)
            .MaximumLength(250);
    }
}
