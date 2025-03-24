using Mapster;
using MapsterMapper;
using Moq;
using TicketTracking.Domain.Dto;
using TicketTracking.Domain.Entities;
using TicketTracking.Domain.Exceptions;
using TicketTracking.Domain.Interfaces;
using TicketTracking.Core.Mapping;
using TicketTracking.Domain.Pagination;
using TicketTracking.Core.Services;

namespace TicketTracking.Core.Tests;

public class TicketServiceTests
{
    private readonly Mock<ITicketRepository> _mockTicketRepository;
    private readonly IMapper _mapper;
    private readonly TicketService _ticketService;

    public TicketServiceTests()
    {
        _mockTicketRepository = new Mock<ITicketRepository>();

        var config = new TypeAdapterConfig();
        MapsterConfiguration.RegisterMappings(config);
        _mapper = new Mapper(config);

        _ticketService = new TicketService(_mockTicketRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetByIdAsync_ExistingTicket_ReturnsTicket()
    {
        // Arrange
        var ticketId = 1L;
        var ticket = new Ticket
        {
            Id = ticketId,
            Title = "Test Ticket",
            Description = "Test Description",
            StatusId = 1,
            Status = new Status { Id = 1, Title = "Open" },
            PriorityId = 1,
            Priority = new Priority { Id = 1, Title = "High" },
            Date = DateTime.UtcNow,
            LastModified = DateTime.UtcNow,
            TicketTypeId = 1,
            TicketType = new TicketType { Id = 1, Title = "Bug" },
            InstalledEnvironmentId = 1
        };

        _mockTicketRepository.Setup(repo => repo.GetByIdAsync(ticketId))
            .ReturnsAsync(ticket);

        // Act
        var result = await _ticketService.GetByIdAsync(ticketId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ticketId, result.Id);
        Assert.Equal(ticket.Title, result.Title);
        Assert.Equal(ticket.Description, result.Description);
        Assert.Equal(ticket.Status.Title, result.Status?.Title);
        Assert.Equal(ticket.Priority.Title, result.Priority?.Title);
    }

    [Fact]
    public async Task GetByIdAsync_NonExistingTicket_ThrowsNotFoundException()
    {
        // Arrange
        var ticketId = 999L;

        _mockTicketRepository.Setup(repo => repo.GetByIdAsync(ticketId))
            .ReturnsAsync((Ticket?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _ticketService.GetByIdAsync(ticketId));

        Assert.Contains($"Ticket with id: {ticketId} was not found!", exception.Message);
    }

    [Fact]
    public async Task GetPagedAsync_ReturnsPagedCollection()
    {
        // Arrange
        var paginationParameters = new PaginationParameters
        {
            PageNumber = 1,
            PageSize = 10
        };

        var tickets = new List<Ticket>
            {
                new Ticket
                {
                    Id = 1,
                    Title = "Ticket 1",
                    Description = "Description 1",
                    StatusId = 1,
                    Status = new Status { Id = 1, Title = "Open" },
                    PriorityId = 1,
                    Priority = new Priority { Id = 1, Title = "High" },
                    TicketTypeId = 1,
                    TicketType = new TicketType { Id = 1, Title = "Bug" }
                },
                new Ticket
                {
                    Id = 2,
                    Title = "Ticket 2",
                    Description = "Description 2",
                    StatusId = 2,
                    Status = new Status { Id = 2, Title = "Closed" },
                    PriorityId = 2,
                    Priority = new Priority { Id = 2, Title = "Low" },
                    TicketTypeId = 2,
                    TicketType = new TicketType { Id = 2, Title = "Feature" }
                }
            };

        var pagedCollection = new PagedCollection<Ticket>
        {
            Items = tickets,
            TotalCount = 2,
            PageNumber = 1,
            PageSize = 10
        };

        _mockTicketRepository.Setup(repo => repo.GetPagedAsync(paginationParameters))
            .ReturnsAsync(pagedCollection);

        // Act
        var result = await _ticketService.GetPagedAsync(paginationParameters);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(1, result.PageNumber);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(2, result.Items.Count());
    }

    [Fact]
    public async Task AddAsync_ValidTicket_ReturnsTicketId()
    {
        // Arrange
        var ticketRequest = new TicketRequestDto
        {
            Title = "New Ticket",
            Description = "New Description",
            ApplicationName = "Test App",
            StatusId = 1,
            PriorityId = 2,
            TicketTypeId = 1,
            InstalledEnvironmentId = 3,
        };

        _mockTicketRepository.Setup(repo => repo.AddAsync(It.IsAny<Ticket>()))
            .Callback<Ticket>(ticket => ticket.Id = 42);

        // Act
        var result = await _ticketService.AddAsync(ticketRequest);

        // Assert
        Assert.Equal(42, result.Id);
        _mockTicketRepository.Verify(repo => repo.AddAsync(It.Is<Ticket>(t =>
            t.Title == ticketRequest.Title &&
            t.Description == ticketRequest.Description &&
            t.ApplicationName == ticketRequest.ApplicationName &&
            t.StatusId == ticketRequest.StatusId &&
            t.PriorityId == ticketRequest.PriorityId &&
            t.TicketTypeId == ticketRequest.TicketTypeId &&
            t.InstalledEnvironmentId == 3)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_ExistingTicket_UpdatesTicket()
    {
        // Arrange
        var ticketId = 1L;
        var ticketRequest = new TicketRequestDto
        {
            Title = "Updated Title",
            Description = "Updated Description",
            ApplicationName = "Updated App",
            StatusId = 2,
            PriorityId = 3,
            TicketTypeId = 2
        };

        var existingTicket = new Ticket
        {
            Id = ticketId,
            Title = "Original Title",
            Description = "Original Description",
            ApplicationName = "Original App",
            StatusId = 1,
            PriorityId = 1,
            TicketTypeId = 1,
            Date = DateTime.UtcNow.AddDays(-1),
            LastModified = DateTime.UtcNow.AddDays(-1)
        };

        _mockTicketRepository.Setup(repo => repo.GetByIdAsync(ticketId))
            .ReturnsAsync(existingTicket);

        DateTime beforeUpdate = DateTime.UtcNow;

        // Act
        await _ticketService.UpdateAsync(ticketId, ticketRequest);

        // Assert
        _mockTicketRepository.Verify(repo => repo.UpdateAsync(It.Is<Ticket>(t =>
            t.Id == ticketId &&
            t.Title == ticketRequest.Title &&
            t.Description == ticketRequest.Description &&
            t.ApplicationName == ticketRequest.ApplicationName &&
            t.StatusId == ticketRequest.StatusId &&
            t.PriorityId == ticketRequest.PriorityId &&
            t.TicketTypeId == ticketRequest.TicketTypeId &&
            t.LastModified >= beforeUpdate)), Times.Once);
    }

    [Fact]
    public async Task UpdateAsync_NonExistingTicket_ThrowsNotFoundException()
    {
        // Arrange
        var ticketId = 999L;
        var ticketRequest = new TicketRequestDto
        {
            Title = "Updated Title",
            Description = "Updated Description",
            ApplicationName = "Test App",
            StatusId = 2,
            PriorityId = 3,
            TicketTypeId = 1
        };

        _mockTicketRepository.Setup(repo => repo.GetByIdAsync(ticketId))
            .ReturnsAsync((Ticket?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(
            () => _ticketService.UpdateAsync(ticketId, ticketRequest));

        Assert.Contains($"Ticket with id: {ticketId} was not found!", exception.Message);
        _mockTicketRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Ticket>()), Times.Never);
    }
}
