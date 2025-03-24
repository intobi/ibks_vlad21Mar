using TicketTracking.Domain.Entities;

namespace TicketTracking.Core.Services.Interfaces;
public interface IReferenceDataService
{
    Task<IEnumerable<TDto>> GetAllAsync<TEntity, TDto>()
        where TEntity : class, IReferenceData;
}
