namespace TicketTracking.Domain.Dto;
public class TicketReplyDto
{
    public int ReplyId { get; set; }

    public long TId { get; set; }

    public string? Reply {  get; set; }

    public DateTime ReplyDate { get; set; }
}
