using TicketTracking.Domain.Entities;

namespace TicketTracking.Domain.Interfaces;
public interface IReplyRepository
{
    Task<IEnumerable<TicketReply>> GetByTicketIdAsync(long ticketId);

    Task AddAsync(TicketReply ticketReply);
}
