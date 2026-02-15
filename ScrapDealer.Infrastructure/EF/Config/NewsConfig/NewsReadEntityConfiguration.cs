using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.NewsConfig
{
    internal class NewsReadEntityConfiguration : IEntityTypeConfiguration<NewsReadModel>
    {
        public void Configure(EntityTypeBuilder<NewsReadModel> builder)
        {
            builder.ToTable("News");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
