using Microsoft.EntityFrameworkCore;
using TicketTracking.Domain.Entities;
using TicketTracking.Domain.Interfaces;

namespace TicketTracking.Infrastructure.Repositories;
public class ReferenceDataRepository : IReferenceDataRepository
{
    private readonly TicketTrackingDbContext _context;

    public ReferenceDataRepository(TicketTrackingDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<T>> GetAllAsync<T>() where T : class, IReferenceData
    {
        return await _context.Set<T>().ToListAsync();
    }
}
