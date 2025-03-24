using MapsterMapper;
using TicketTracking.Domain.Dto;
using TicketTracking.Domain.Entities;
using TicketTracking.Domain.Exceptions;
using TicketTracking.Domain.Interfaces;
using TicketTracking.Domain.Pagination;
using TicketTracking.Core.Services.Interfaces;

namespace TicketTracking.Core.Services;
public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IMapper _mapper;

    public TicketService(ITicketRepository ticketRepository, IMapper mapper)
    {
        _ticketRepository = ticketRepository;
        _mapper = mapper;
    }

    public async Task<TicketDto> GetByIdAsync(long id)
    {
        var ticket = await _ticketRepository.GetByIdAsync(id) 
            ?? throw new NotFoundException($"Ticket with id: {id} was not found!");

        return _mapper.Map<TicketDto>(ticket);;
    }

    public async Task<PagedCollection<TicketListItemDto>> GetPagedAsync(PaginationParameters paginationParameters)
    {
        var pagedCollection = await _ticketRepository.GetPagedAsync(paginationParameters);
        return _mapper.Map<PagedCollection<TicketListItemDto>>(pagedCollection);
    }

    public async Task<AddTicketResponseDto> AddAsync(TicketRequestDto request)
    {
        var newTicket = _mapper.Map<Ticket>(request);
        newTicket.Date = DateTime.UtcNow;

        await _ticketRepository.AddAsync(newTicket);

        return new AddTicketResponseDto { Id = newTicket.Id };
    }

    public async Task UpdateAsync(long id, TicketRequestDto request)
    {
        var existingTicket = await _ticketRepository.GetByIdAsync(id)
            ?? throw new NotFoundException($"Ticket with id: {id} was not found!");

        _mapper.Map(request, existingTicket);
        existingTicket.LastModified = DateTime.UtcNow;

        await _ticketRepository.UpdateAsync(existingTicket);
    }
}
