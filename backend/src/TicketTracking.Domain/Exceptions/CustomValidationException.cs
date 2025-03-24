namespace TicketTracking.Domain.Exceptions;
public class CustomValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public CustomValidationException(IDictionary<string, string[]> errors)
    {
        Errors = errors;
    }
}
