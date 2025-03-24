using Microsoft.EntityFrameworkCore;
using TicketTracking.Domain.Entities;
using TicketTracking.Domain.Interfaces;

namespace TicketTracking.Infrastructure.Repositories;
public class ReplyRepository : IReplyRepository
{
    private readonly TicketTrackingDbContext _context;

    public ReplyRepository(TicketTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TicketReply>> GetByTicketIdAsync(long ticketId)
    {
        return await _context.TicketReplies.Where(tr => tr.TId == ticketId)
            .ToListAsync();
    }

    public async Task AddAsync(TicketReply ticketReplyDto)
    {
        _context.Add(ticketReplyDto);
        await _context.SaveChangesAsync();
    }
}
