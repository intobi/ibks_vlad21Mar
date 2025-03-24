using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracking.Domain.Entities;

namespace TicketTracking.Infrastructure.Configuration
{
    public class TicketEventLogConfiguration : IEntityTypeConfiguration<TicketEventLog>
    {
        public void Configure(EntityTypeBuilder<TicketEventLog> builder)
        {
            builder.ToTable("TicketEventLog", "Support");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .UseIdentityColumn();

            builder.Property(e => e.LogTypeId)
                .IsRequired();

            builder.Property(e => e.TicketId)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.Property(e => e.EventDt)
                .IsRequired();

            builder.Property(e => e.Message)
                .IsRequired(false);

            builder.HasOne(e => e.LogType)
                .WithMany()
                .HasForeignKey(e => e.LogTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Ticket)
                .WithMany()
                .HasForeignKey(e => e.TicketId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}