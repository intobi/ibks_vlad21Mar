namespace TicketTracking.Domain.Dto;
public class TicketRequestDto
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public string? Url { get; set; }

    public string? ApplicationName { get; set; }

    public string? StackTrace { get; set; }

    public string? Device { get; set; }

    public string? Browser { get; set; }

    public string? Resolution { get; set; }

    public int PriorityId { get; set; }

    public int TicketTypeId { get; set; }

    public int StatusId { get; set; }

    public string? UserOID { get; set; }

    public int InstalledEnvironmentId { get; set; }
}
