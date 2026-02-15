using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Tickets;

namespace ScrapDealer.Infrastructure.EF.Config.Tickets
{
    internal class TicketWriteEntityConfiguration : IEntityTypeConfiguration<Ticket>, IEntityTypeConfiguration<TicketMessage>
    {
        public void Configure(EntityTypeBuilder<TicketMessage> builder)
        {
            builder.ToTable("TicketMessages");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.Property(u => u.Content)
                .HasConversion(content => content.Value, content => MessageContent.Create(content))
                .IsRequired();

            builder.Property(s => s.Attachments)
            .HasConversion(
                v => string.Join(",", v),
                v => string.IsNullOrWhiteSpace(v)
                    ? new List<Guid>()
                    : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
            )
            .HasColumnType("nvarchar(max)");
        }

        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Tickets");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.Property(u => u.Title)
                .HasConversion(title => title.Value, title => TicketTitle.Create(title))
                .IsRequired();

            builder.HasMany(u => u.Messages)
                .WithOne(t=> t.Ticket)
                .HasForeignKey(ur => ur.TicketId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
