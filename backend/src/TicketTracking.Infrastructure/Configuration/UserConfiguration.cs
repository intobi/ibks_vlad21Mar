using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracking.Domain.Entities;

namespace TicketTracking.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Support");

            builder.HasKey(e => e.Oid);

            builder.Property(e => e.Oid)
                .HasColumnName("OID")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(e => e.Email)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(e => e.LastScannedUtc)
                .IsRequired(false);
        }
    }
}