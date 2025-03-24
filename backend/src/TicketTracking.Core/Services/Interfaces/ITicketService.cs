using TicketTracking.Domain.Dto;
using TicketTracking.Domain.Pagination;

namespace TicketTracking.Core.Services.Interfaces;
public interface ITicketService
{
    Task<TicketDto> GetByIdAsync(long id);

    Task<PagedCollection<TicketListItemDto>> GetPagedAsync(PaginationParameters paginationParameters);

    Task<AddTicketResponseDto> AddAsync(TicketRequestDto request);

    Task UpdateAsync(long id, TicketRequestDto request);
}
