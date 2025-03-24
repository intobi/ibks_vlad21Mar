namespace TicketTracking.Domain.Entities;
public class Priority : IReferenceData
{
    public int Id { get; set; }
    public string? Title { get; set; }
}
