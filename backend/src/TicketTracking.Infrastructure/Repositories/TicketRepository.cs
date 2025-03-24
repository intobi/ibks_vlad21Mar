using Microsoft.EntityFrameworkCore;
using TicketTracking.Domain.Entities;
using TicketTracking.Domain.Interfaces;
using TicketTracking.Domain.Pagination;
using TicketTracking.Infrastructure.Extensions;

namespace TicketTracking.Infrastructure.Repositories;
public class TicketRepository : ITicketRepository
{
    private readonly TicketTrackingDbContext _context;

    private IQueryable<Ticket> Query =>
        _context.Tickets.Where(x => !x.Deleted.HasValue || (x.Deleted.HasValue && !x.Deleted.Value));

    public TicketRepository(TicketTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<Ticket?> GetByIdAsync(long id)
    {
        return await Query.Include(x => x.Status)
            .Include(x => x.Priority)
            .Include(x => x.TicketType)
            .Include(x => x.InstalledEnvironment)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<PagedCollection<Ticket>> GetPagedAsync(PaginationParameters parameters)
    {
        return await Query.Include(x => x.Status)
            .Include(x => x.Priority)
            .Include(x => x.TicketType)
            .ToPagedResponseAsync(parameters.PageNumber, parameters.PageSize);
    }

    public async Task AddAsync(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();
    } 

    public async Task UpdateAsync(Ticket ticket)
    {
        _context.Entry(ticket).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
