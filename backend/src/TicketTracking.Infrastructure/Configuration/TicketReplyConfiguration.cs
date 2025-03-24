using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicketTracking.Domain.Entities;

namespace TicketTracking.Infrastructure.Configuration
{
    public class TicketReplyConfiguration : IEntityTypeConfiguration<TicketReply>
    {
        public void Configure(EntityTypeBuilder<TicketReply> builder)
        {
            builder.ToTable("TicketReply", "Support");

            builder.HasKey(e => e.ReplyId);

            builder.Property(e => e.ReplyId)
                .HasColumnName("ReplyId")
                .UseIdentityColumn();

            builder.Property(e => e.TId)
                .HasColumnName("TId")
                .IsRequired();

            builder.Property(e => e.Reply)
                .IsRequired(false);

            builder.Property(e => e.ReplyDate)
                .IsRequired();
        }
    }
}