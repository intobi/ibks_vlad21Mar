namespace TicketTracking.Domain.Dto;
public class UserDto
{
    public string Oid { get; set; } = null!;
    public string? DisplayName { get; set; }
    public string? Email { get; set; }
    public string? FullName { get; set; }
    public DateTime? LastScannedUtc { get; set; }
}
