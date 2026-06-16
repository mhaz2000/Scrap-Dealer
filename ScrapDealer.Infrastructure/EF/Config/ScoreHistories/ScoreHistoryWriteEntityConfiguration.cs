using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Infrastructure.EF.Config.ScoreHistories;

internal class ScoreHistoryWriteEntityConfiguration : IEntityTypeConfiguration<ScoreHistory>
{
    public void Configure(EntityTypeBuilder<ScoreHistory> builder)
    {
        builder.ToTable("ScoreHistories");

        builder.Property(u => u.Score)
            .HasConversion(score => score.Value, score => Score.Create(score))
            .IsRequired();

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