using MapsterMapper;
using TicketTracking.Domain.Dto;
using TicketTracking.Domain.Entities;
using TicketTracking.Domain.Exceptions;
using TicketTracking.Domain.Interfaces;
using TicketTracking.Core.Services.Interfaces;

namespace TicketTracking.Core.Services;
public class ReplyService : IReplyService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IReplyRepository _replyRepository;
    private readonly IMapper _mapper;

    public ReplyService(
        IReplyRepository replyRepository, 
        IMapper mapper, 
        ITicketRepository ticketRepository)
    {
        _replyRepository = replyRepository;
        _mapper = mapper;
        _ticketRepository = ticketRepository;
    }

    public async Task<int> AddAsync(TicketReplyDto dto)
    {
        _ = await _ticketRepository.GetByIdAsync(dto.TId)
            ?? throw new NotFoundException($"Ticket with id: {dto.TId} was not found!");

        var entity = _mapper.Map<TicketReply>(dto);
        entity.ReplyDate = DateTime.UtcNow;

        await _replyRepository.AddAsync(entity);
        return entity.ReplyId;
    }

    public async Task<IEnumerable<TicketReplyDto>> GetByTicketIdAsync(long ticketId)
    {
        var replies = await _replyRepository.GetByTicketIdAsync(ticketId);
        return _mapper.Map<IEnumerable<TicketReplyDto>>(replies);
    }
}
