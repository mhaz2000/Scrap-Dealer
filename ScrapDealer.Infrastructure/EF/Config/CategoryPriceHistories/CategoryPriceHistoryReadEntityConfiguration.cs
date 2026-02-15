using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.CategoryPriceHistories
{
    internal class CategoryPriceHistoryReadEntityConfiguration : IEntityTypeConfiguration<CategoryPriceHistoryReadModel>
    {
        public void Configure(EntityTypeBuilder<CategoryPriceHistoryReadModel> builder)
        {
            builder.ToTable("CategoryPriceHistories");
            builder.HasKey(x => x.Id);


            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
