using TicketTracking.Domain.Entities;
using TicketTracking.Domain.Pagination;

namespace TicketTracking.Domain.Interfaces;
public interface ITicketRepository
{
    Task<Ticket?> GetByIdAsync(long id);

    Task<PagedCollection<Ticket>> GetPagedAsync(PaginationParameters parameters);

    Task AddAsync(Ticket ticket);

    Task UpdateAsync(Ticket ticket);
}
