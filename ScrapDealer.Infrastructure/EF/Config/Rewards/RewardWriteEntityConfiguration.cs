using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Infrastructure.EF.Config.Rewards
{
    public class RewardWriteEntityConfiguration : IEntityTypeConfiguration<Reward>
    {
        public void Configure(EntityTypeBuilder<Reward> builder)
        {
            builder.ToTable("Rewards");

            builder.Property(u => u.Amount)
                .HasConversion(amount => amount.Value, amount => Amount.Create(amount))
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
