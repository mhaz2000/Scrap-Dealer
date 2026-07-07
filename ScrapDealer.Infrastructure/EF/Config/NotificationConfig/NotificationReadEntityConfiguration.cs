using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Models;
using System.Text.Json;

namespace ScrapDealer.Infrastructure.EF.Config.NotificationConfig
{
    internal class NotificationReadEntityConfiguration : IEntityTypeConfiguration<NotificationReadModel>
    {
        public void Configure(EntityTypeBuilder<NotificationReadModel> builder)
        {
            builder.ToTable("Notifications");


            builder.Property(x => x.SeenBy)
           .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => string.IsNullOrWhiteSpace(v) ? new() : JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null) ?? new())
            .HasColumnType("nvarchar(max)");

            builder.Property(x => x.Targets)
                .HasConversion(
                    targets => JsonSerializer.Serialize(targets, (JsonSerializerOptions?)null),
                    targets => string.IsNullOrWhiteSpace(targets) ? new() : JsonSerializer.Deserialize<List<NotificationTarget>>(targets, (JsonSerializerOptions?)null) ?? new())
                .HasColumnType("nvarchar(max)");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
