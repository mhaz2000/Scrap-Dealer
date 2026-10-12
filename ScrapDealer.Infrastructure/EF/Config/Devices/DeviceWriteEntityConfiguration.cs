using ScrapDealer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrapDealer.Infrastructure.EF.Config.Devices
{
    internal class DeviceWriteEntityConfiguration : IEntityTypeConfiguration<UserDeviceToken>
    {
        public void Configure(EntityTypeBuilder<UserDeviceToken> builder)
        {
            builder.ToTable("UserDeviceTokens");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasIndex(x => x.UserId);
            builder.HasIndex(x => new { x.UserId, x.Token })
                .IsUnique();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
