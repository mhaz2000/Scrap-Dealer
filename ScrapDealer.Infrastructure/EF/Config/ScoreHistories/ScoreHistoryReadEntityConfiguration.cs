using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.ScoreHistories;


internal class ScoreHistoryReadEntityConfiguration : IEntityTypeConfiguration<ScoreHistoryReadModel>
{
    public void Configure(EntityTypeBuilder<ScoreHistoryReadModel> builder)
    {
        builder.ToTable("ScoreHistories");

        builder.HasOne(u => u.Buyer)
            .WithMany()
            .HasForeignKey(u => u.BuyerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(u => u.Seller)
            .WithMany()
            .HasForeignKey(u => u.SellerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(u => u.Contract)
            .WithMany()
            .HasForeignKey(u => u.ContractId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }
}
