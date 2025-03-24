using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracking.Domain.Entities;

namespace TicketTracking.Infrastructure.Configuration
{
    public class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket", "Support");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .UseIdentityColumn();

            builder.Property(e => e.Title)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(e => e.ApplicationId)
                .IsRequired();

            builder.Property(e => e.ApplicationName)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(e => e.Description)
                .IsRequired(false);

            builder.Property(e => e.Url)
                .HasMaxLength(1000)
                .IsRequired(false);

            builder.Property(e => e.StackTrace)
                .IsRequired(false);

            builder.Property(e => e.Device)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(e => e.Browser)
                .HasMaxLength(250)
                .IsRequired(false);

            builder.Property(e => e.Resolution)
                .IsRequired(false);

            builder.Property(e => e.PriorityId)
                .IsRequired();

            builder.Property(e => e.StatusId)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired(false);

            builder.Property(e => e.UserOID)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(e => e.InstalledEnvironmentId)
                .IsRequired();

            builder.Property(e => e.TicketTypeId)
                .IsRequired();

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Deleted)
                .IsRequired(false);

            builder.Property(e => e.LastModified)
                .IsRequired();

            builder.Property(e => e.CreatedByOID)
                .IsRequired(false);

            builder.HasOne(e => e.Priority)
                .WithMany()
                .HasForeignKey(e => e.PriorityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.Status)
                .WithMany()
                .HasForeignKey(e => e.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(e => e.UserOID)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.InstalledEnvironment)
                .WithMany()
                .HasForeignKey(e => e.InstalledEnvironmentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(e => e.TicketType)
                .WithMany()
                .HasForeignKey(e => e.TicketTypeId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}