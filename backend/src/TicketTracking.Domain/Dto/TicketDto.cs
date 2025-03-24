namespace TicketTracking.Domain.Dto;
public class TicketDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? Url { get; set; }

    public string? Description { get; set; }

    public string? ApplicationName { get; set; }

    public string? StackTrace { get; set; }

    public string? Device { get; set; }

    public string? Browser { get; set; }

    public string? Resolution { get; set; }

    public DateTime Date { get; set; }

    public DateTime LastModified { get; set; }

    public PriorityDto? Priority { get; set; }

    public TicketTypeDto? TicketType { get; set; }

    public StatusDto? Status { get; set; }

    public UserDto? User { get; set; }

    public InstalledEnvironmentDto? InstalledEnvironment { get; set; }
}
