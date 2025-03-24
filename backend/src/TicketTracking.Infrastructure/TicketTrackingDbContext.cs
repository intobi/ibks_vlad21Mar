using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TicketTracking.Domain.Entities;

namespace TicketTracking.Infrastructure;
public class TicketTrackingDbContext : DbContext
{
    public TicketTrackingDbContext(DbContextOptions<TicketTrackingDbContext> options)
        : base(options)
    {
    }

    public DbSet<InstalledEnvironment> InstalledEnvironments { get; set; }
    public DbSet<LogType> LogTypes { get; set; }
    public DbSet<Priority> Priorities { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<TicketEventLog> TicketEventLogs { get; set; }
    public DbSet<TicketReply> TicketReplies { get; set; }
    public DbSet<TicketType> TicketTypes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
