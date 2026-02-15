using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Rewards
{
    internal class RewardReadEntityConfiguration : IEntityTypeConfiguration<RewardReadModel>
    {
        public void Configure(EntityTypeBuilder<RewardReadModel> builder)
        {
            builder.ToTable("Rewards");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
