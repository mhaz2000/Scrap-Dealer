using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

public class SettingsReadEntityConfiguration : IEntityTypeConfiguration<SettingsReadModel>
{
    void IEntityTypeConfiguration<SettingsReadModel>.Configure(EntityTypeBuilder<SettingsReadModel> builder)
    {
        builder.ToTable("Settings");

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
