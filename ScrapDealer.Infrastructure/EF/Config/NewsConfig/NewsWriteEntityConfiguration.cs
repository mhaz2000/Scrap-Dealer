using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.News;

namespace ScrapDealer.Infrastructure.EF.Config.NewsConfig
{
    internal class NewsWriteEntityConfiguration : IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.ToTable("News");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Title)
                .HasConversion(title => title.Value, title => NewsTitle.Create(title))
                .IsRequired();

            builder.Property(u => u.Summary)
                .HasConversion(summary => summary.Value, summary => NewsSummary.Create(summary))
                .IsRequired();

            builder.Property(u => u.Content)
                .HasConversion(content => content.Value, content => NewsContent.Create(content))
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
