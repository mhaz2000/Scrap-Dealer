using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Notifications;
using System.Text.Json;

namespace ScrapDealer.Infrastructure.EF.Config.NotificationConfig
{

    internal class NotificationWriteEntityConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Title)
                .HasConversion(title => title.Value, title => Title.Create(title))
                .IsRequired();

            builder.Property(u => u.Content)
                .HasConversion(content => content.Value, content => NotificationContent.Create(content))
                .IsRequired();

            builder.Property(x => x.SeenBy)
           .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => v == null || v != "[]" ? JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions?)null) ?? new() : new())
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
