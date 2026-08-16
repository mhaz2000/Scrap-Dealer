using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.News;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ScrapDealer.Infrastructure.EF.Config.NewsConfig
{
    internal class NewsWriteEntityConfiguration : IEntityTypeConfiguration<News>
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            Converters = { new JsonStringEnumConverter() }
        };

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
                .HasConversion(
                    content => JsonSerializer.Serialize(content.Blocks, _jsonOptions),
                    content => NewsContent.Create(JsonSerializer.Deserialize<List<NewsContentBlock>>(content, _jsonOptions) ?? new List<NewsContentBlock>()))
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}