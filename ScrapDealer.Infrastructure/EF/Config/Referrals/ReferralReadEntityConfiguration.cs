using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Referrals
{
    internal class ReferralReadEntityConfiguration : IEntityTypeConfiguration<ReferralReadModel>
    {
        public void Configure(EntityTypeBuilder<ReferralReadModel> builder)
        {
            builder.ToTable("Referrals");

            builder.HasKey(r => r.Id);

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasIndex(r => r.RefereeUserId)
                .IsUnique();

            builder.HasOne(r => r.ReferrerUser)
                .WithMany()
                .HasForeignKey(r => r.ReferrerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.RefereeUser)
                .WithMany()
                .HasForeignKey(r => r.RefereeUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
