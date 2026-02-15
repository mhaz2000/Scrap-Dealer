using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Infrastructure.EF.Config.CategoryPriceHistories
{
    internal class CategoryPriceHistoryWriteEntityConfiguration : IEntityTypeConfiguration<CategoryPriceHistory>
    {
        public void Configure(EntityTypeBuilder<CategoryPriceHistory> builder)
        {
            builder.ToTable("CategoryPriceHistories");
            builder.HasKey(x => x.Id);

            builder.OwnsOne(x => x.PriceRange, pr =>
            {
                pr.Property(p => p.MinValue)
                    .HasColumnName("MinPrice")
                    .IsRequired();

                pr.Property(p => p.MaxValue)
                    .HasColumnName("MaxPrice")
                    .IsRequired();
            });

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
