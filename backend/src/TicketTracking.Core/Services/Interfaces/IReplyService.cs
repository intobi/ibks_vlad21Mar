using TicketTracking.Domain.Dto;

namespace TicketTracking.Core.Services.Interfaces;
public interface IReplyService
{
    Task<IEnumerable<TicketReplyDto>> GetByTicketIdAsync(long ticketId);

    Task<int> AddAsync(TicketReplyDto dto);
}
    