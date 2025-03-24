using TicketTracking.Domain.Entities;

namespace TicketTracking.Domain.Interfaces;
public interface IReferenceDataRepository
{
    Task<IEnumerable<T>> GetAllAsync<T>() where T : class, IReferenceData;
}
