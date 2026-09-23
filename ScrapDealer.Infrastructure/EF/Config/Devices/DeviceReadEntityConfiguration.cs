using ScrapDealer.Domain.Consts;
using ScrapDealer.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrapDealer.Infrastructure.EF.Config.Devices
{
    internal class DeviceReadEntityConfiguration : IEntityTypeConfiguration<DeviceReadModel>
    {
        public void Configure(EntityTypeBuilder<DeviceReadModel> builder)
        {
            builder.ToTable("UserDeviceTokens");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token)
                .HasMaxLength(500)
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}