using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.SaleOrders
{
    internal class SaleOrderReadEntityConfiguration : IEntityTypeConfiguration<SaleOrderReadModel>
    {
        public void Configure(EntityTypeBuilder<SaleOrderReadModel> builder)
        {
            builder.ToTable("SaleOrders");
            builder.HasKey(x => x.Id);

            builder.HasOne(u => u.Seller)
                .WithMany()
                .HasForeignKey(u => u.SellerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(s => s.Items)
                .WithOne(s => s.SaleOrder)
                .HasForeignKey(t => t.SaleOrderId);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
