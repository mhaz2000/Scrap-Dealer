using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Tickets
{

    internal class TicketReadEntityConfiguration : IEntityTypeConfiguration<TicketReadModel>, IEntityTypeConfiguration<TicketMessageReadModel>
    {
        public void Configure(EntityTypeBuilder<TicketMessageReadModel> builder)
        {
            builder.ToTable("TicketMessages");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.Property(s => s.Attachments)
            .HasConversion(
                v => string.Join(",", v),
                v => string.IsNullOrWhiteSpace(v)
                    ? new List<Guid>()
                    : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
            )
            .HasColumnType("nvarchar(max)");

        }

        public void Configure(EntityTypeBuilder<TicketReadModel> builder)
        {
            builder.ToTable("Tickets");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
