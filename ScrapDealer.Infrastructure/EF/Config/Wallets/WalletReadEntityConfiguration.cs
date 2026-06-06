using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Wallets;

internal class WalletReadEntityConfiguration : IEntityTypeConfiguration<WalletReadModel>
{
    public void Configure(EntityTypeBuilder<WalletReadModel> builder)
    {
        builder.ToTable("Wallets");

        builder.HasQueryFilter(p => !p.IsDeleted);

        builder.HasOne(x => x.Buyer)
            .WithMany()
            .HasForeignKey(x => x.BuyerId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasOne(x => x.Seller)
            .WithMany()
            .HasForeignKey(x => x.SellerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
