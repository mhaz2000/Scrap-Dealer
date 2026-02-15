using ScrapDealer.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrapDealer.Infrastructure.EF.Config.Buyers
{
    internal class SellerReadEntityConfiguration : IEntityTypeConfiguration<SellerReadModel>
    {
        public void Configure(EntityTypeBuilder<SellerReadModel> builder)
        {
            builder.ToTable("Sellers");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
