using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;

public class SettingsWriteEntityConfiguration : IEntityTypeConfiguration<Settings>
{
    public void Configure(EntityTypeBuilder<Settings> builder)
    {
        builder.ToTable("Settings");

        builder.HasKey(x => x.Id);

        builder.OwnsOne(x => x.BuyerCommissionRate, rate =>
        {
            rate.Property(x => x.Value)
                .HasColumnName("BuyerCommissionRate")
                .HasPrecision(5, 2); // e.g. 100.00

            rate.WithOwner();
        });

        builder.OwnsOne(x => x.BuyerCommissionFixedAmount, amount =>
        {
            amount.Property(x => x.Value)
                .HasColumnName("BuyerCommissionFixedAmount")
                .HasPrecision(18, 2);

            amount.WithOwner();
        });

        // Allow null ValueObjects
        builder.Navigation(x => x.BuyerCommissionRate).IsRequired(false);
        builder.Navigation(x => x.BuyerCommissionFixedAmount).IsRequired(false);
    }
}
