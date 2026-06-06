using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Wallets;

internal class WalletTransactionReadEntityConfiguration : IEntityTypeConfiguration<WalletTransactionReadModel>
{
    public void Configure(EntityTypeBuilder<WalletTransactionReadModel> builder)
    {
        builder.ToTable("WalletTransactions");

        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasOne(x => x.Wallet)
            .WithMany()
            .HasForeignKey(x => x.WalletId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}

