using ScrapDealer.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrapDealer.Infrastructure.EF.Config.Buyers
{
    internal class BuyerReadEntityConfiguration : IEntityTypeConfiguration<BuyerReadModel>
    {
        public void Configure(EntityTypeBuilder<BuyerReadModel> builder)
        {
            builder.ToTable("Buyers");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.Property(x => x.Latitude).HasColumnName("Latitude");
            builder.Property(x => x.Longitude).HasColumnName("Longitude");

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.LocationImages)
                .HasConversion(
                    v => string.Join(",", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? new List<Guid>()
                        : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
                )
                .HasColumnType("nvarchar(max)");
        }
    }
}
