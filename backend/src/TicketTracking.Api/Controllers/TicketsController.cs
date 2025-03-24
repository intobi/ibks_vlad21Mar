using Microsoft.AspNetCore.Mvc;
using TicketTracking.Domain.Dto;
using TicketTracking.Domain.Pagination;
using TicketTracking.Core.Services.Interfaces;

namespace TicketTracking.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class TicketsController : ControllerBase
{
    private readonly ITicketService _ticketService;

    public TicketsController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    /// <summary>
    /// Gets a ticket by ID
    /// </summary>
    /// <param name="id">The ticket ID</param>
    /// <returns>The ticket details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TicketDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TicketDto>> GetById(int id)
    {
        var ticket = await _ticketService.GetByIdAsync(id);

        if (ticket == null)
        {
            return NotFound();
        }

        return Ok(ticket);
    }

    /// <summary>
    /// Gets a paged list of tickets
    /// </summary>
    /// <param name="page">Page number (starting from 1)</param>
    /// <param name="pageSize">Number of items per page</param>
    /// <returns>A paged collection of tickets</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedCollection<TicketListItemDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedCollection<TicketListItemDto>>> GetPaged(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var paginationParameters = new PaginationParameters
        {
            PageNumber = page,
            PageSize = pageSize
        };

        var tickets = await _ticketService.GetPagedAsync(paginationParameters);
        return Ok(tickets);
    }

    /// <summary>
    /// Creates a new ticket
    /// </summary>
    /// <param name="request">The ticket creation request details</param>
    /// <returns>A 201 Created response when the ticket is successfully created</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AddTicketResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTicket([FromBody] TicketRequestDto request)
    {
        var response = await _ticketService.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    /// <summary>
    /// Updates an existing ticket
    /// </summary>
    /// <param name="id">The ticket ID</param>
    /// <param name="request">The update request</param>
    /// <returns>No content on success</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(int id, TicketRequestDto request)
    {
        await _ticketService.UpdateAsync(id, request);
        return NoContent();
    }
}
