using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracking.Domain.Entities;

namespace TicketTracking.Infrastructure.Configuration;
public class InstalledEnvironmentConfiguration : IEntityTypeConfiguration<InstalledEnvironment>
{
    public void Configure(EntityTypeBuilder<InstalledEnvironment> builder)
    {
        builder.ToTable("InstalledEnvironment", "Support");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .UseIdentityColumn();

        builder.Property(e => e.Title)
            .HasMaxLength(250)
            .IsRequired(false);
    }
}
