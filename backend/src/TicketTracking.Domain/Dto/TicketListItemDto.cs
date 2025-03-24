namespace TicketTracking.Domain.Dto;
public class TicketListItemDto
{
    public int Id { get; set; }

    public string? Title { get; set; }

    public string? ApplicationName { get; set; }

    public PriorityDto? Priority { get; set; }

    public TicketTypeDto? TicketType { get; set; }

    public StatusDto? Status { get; set; }
}
