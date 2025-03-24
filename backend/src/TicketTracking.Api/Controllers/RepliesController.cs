using Microsoft.AspNetCore.Mvc;
using TicketTracking.Domain.Dto;
using TicketTracking.Core.Services.Interfaces;

namespace TicketTracking.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RepliesController : ControllerBase
{
    private readonly IReplyService _replyService;

    public RepliesController(IReplyService replyService)
    {
        _replyService = replyService;
    }

    /// <summary>
    /// Gets all replies for a specific ticket
    /// </summary>
    /// <param name="ticketId">ID of the ticket</param>
    /// <returns>Collection of ticket replies</returns>
    [HttpGet("ticket/{ticketId}")]
    [ProducesResponseType(typeof(IEnumerable<TicketReplyDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TicketReplyDto>>> GetByTicketId(long ticketId)
    {
        var replies = await _replyService.GetByTicketIdAsync(ticketId);
        return Ok(replies);
    }

    /// <summary>
    /// Adds a new reply to a ticket
    /// </summary>
    /// <param name="replyDto">The reply information</param>
    /// <returns>ID of the newly created reply</returns>
    [HttpPost]
    [ProducesResponseType(typeof(int), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<int>> AddReply(TicketReplyDto replyDto)
    {
        var replyId = await _replyService.AddAsync(replyDto);

        return CreatedAtAction(
            nameof(GetByTicketId),
            new { ticketId = replyDto.TId },
            replyId);
    }
}
