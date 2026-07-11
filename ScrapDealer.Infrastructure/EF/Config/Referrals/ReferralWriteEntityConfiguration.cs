using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Infrastructure.EF.Config.Referrals
{
    public class ReferralWriteEntityConfiguration : IEntityTypeConfiguration<Referral>
    {
        public void Configure(EntityTypeBuilder<Referral> builder)
        {
            builder.ToTable("Referrals");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Code)
                .IsRequired();

            builder.Property(r => r.Status)
                .IsRequired();

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.ReferrerUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.RefereeUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
