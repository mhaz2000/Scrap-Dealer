using ScrapDealer.Infrastructure.EF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ScrapDealer.Infrastructure.EF.Config.Buyers
{
    internal class BuyerReadEntityConfiguration : IEntityTypeConfiguration<BuyerReadModel>
    {
        public void Configure(EntityTypeBuilder<BuyerReadModel> builder)
        {
            builder.ToTable("Buyers");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
