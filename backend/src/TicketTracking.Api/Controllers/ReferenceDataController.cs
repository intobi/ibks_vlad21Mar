using Microsoft.AspNetCore.Mvc;
using TicketTracking.Domain.Dto;
using TicketTracking.Domain.Entities;
using TicketTracking.Core.Services.Interfaces;

namespace TicketTracking.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ReferenceDataController : ControllerBase
{
    private readonly IReferenceDataService _service;

    public ReferenceDataController(IReferenceDataService service)
    {
        _service = service;
    }

    [HttpGet("priorities")]
    public async Task<ActionResult<IEnumerable<PriorityDto>>> GetPriorities()
    {
        return Ok(await _service.GetAllAsync<Priority, PriorityDto>());
    }

    [HttpGet("statuses")]
    public async Task<ActionResult<IEnumerable<StatusDto>>> GetStatuses()
    {
        return Ok(await _service.GetAllAsync<Status, StatusDto>());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TicketTypeDto>>> GetTypes()
    {
        return Ok(await _service.GetAllAsync<TicketType, TicketTypeDto>());
    }

    [HttpGet("installed-environments")]
    public async Task<ActionResult<IEnumerable<InstalledEnvironmentDto>>> GetInstalledEnvironments()
    {
        return Ok(await _service.GetAllAsync<InstalledEnvironment, InstalledEnvironmentDto>());
    }

    [HttpGet("users")]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        return Ok(await _service.GetAllAsync<User, UserDto>());
    }
}
