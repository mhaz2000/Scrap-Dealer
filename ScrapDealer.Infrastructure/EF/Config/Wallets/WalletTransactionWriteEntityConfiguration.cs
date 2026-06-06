using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Infrastructure.EF.Config.Wallets;

internal class WalletTransactionWriteEntityConfiguration : IEntityTypeConfiguration<WalletTransaction>
{
    public void Configure(EntityTypeBuilder<WalletTransaction> builder)
    {
        builder.ToTable("WalletTransactions");
        builder.HasKey(x => x.Id);

        builder.Property(u => u.Amount)
            .HasConversion(amount => amount.Value, amount => Amount.Create(amount))
            .IsRequired();

        builder.Property(u => u.Description)
            .HasConversion(description => description.Value, description => Description.Create(description))
            .IsRequired();

        builder.HasOne(u => u.Wallet)
            .WithMany()
            .HasForeignKey(ur => ur.WalletId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}