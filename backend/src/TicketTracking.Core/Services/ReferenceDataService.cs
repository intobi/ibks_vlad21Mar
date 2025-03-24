using MapsterMapper;
using TicketTracking.Domain.Entities;
using TicketTracking.Domain.Interfaces;
using TicketTracking.Core.Services.Interfaces;

namespace TicketTracking.Core.Services;
public class ReferenceDataService : IReferenceDataService
{
    private readonly IReferenceDataRepository _repository;
    private readonly IMapper _mapper;

    public ReferenceDataService(IReferenceDataRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TDto>> GetAllAsync<TEntity, TDto>()
        where TEntity : class, IReferenceData
    {
        var entities = await _repository.GetAllAsync<TEntity>();
        return _mapper.Map<IEnumerable<TDto>>(entities);
    }
}
