using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracking.Domain.Entities;

namespace TicketTracking.Infrastructure.Configuration
{
    public class StatusConfiguration : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.ToTable("Status", "Support");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id);

            builder.Property(e => e.Title)
                .HasMaxLength(250)
                .IsRequired(false);
        }
    }
}