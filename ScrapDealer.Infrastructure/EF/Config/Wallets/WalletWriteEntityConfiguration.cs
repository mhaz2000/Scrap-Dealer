using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.Wallets;

namespace ScrapDealer.Infrastructure.EF.Config.Wallets;

internal class WalletWriteEntityConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.ToTable("Wallets");
        builder.HasKey(x => x.Id);

        builder.Property(u => u.Number)
            .HasConversion(number => number.Value, number => WalletNumber.Create(number))
            .IsRequired();

        builder.Property(u => u.Balance)
            .HasConversion(balance => balance.Value, balance => Amount.Create(balance))
            .IsRequired();

        builder.HasOne(u => u.Buyer)
            .WithMany()
            .HasForeignKey(ur => ur.BuyerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(u => u.Seller)
            .WithMany()
            .HasForeignKey(ur => ur.SellerId)
            .OnDelete(DeleteBehavior.Cascade);


        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
