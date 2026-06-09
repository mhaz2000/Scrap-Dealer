using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;

namespace ScrapDealer.Infrastructure.EF.Config.SaleOrderRequests;

internal class SaleOrderRequestWriteEntityConfiguration : IEntityTypeConfiguration<SaleOrderRequest>
{
    public void Configure(EntityTypeBuilder<SaleOrderRequest> builder)
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