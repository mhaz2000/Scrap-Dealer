using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.SaleOrderRequests;
internal class SaleOrderRequestReadEntityConfiguration : IEntityTypeConfiguration<SaleOrderRequestReadModel>
{
    public void Configure(EntityTypeBuilder<SaleOrderRequestReadModel> builder)
    {
        builder.ToTable("SaleOrderRequests");

        builder.HasOne(x => x.Buyer)
            .WithMany()
            .HasForeignKey(x => x.BuyerId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.SaleOrder)
            .WithMany()
            .HasForeignKey(x => x.SaleOrderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
