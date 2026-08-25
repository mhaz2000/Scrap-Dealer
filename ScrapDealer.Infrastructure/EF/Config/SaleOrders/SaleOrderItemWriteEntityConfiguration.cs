using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Infrastructure.EF.Config.SaleOrders
{
    internal class SaleOrderItemWriteEntityConfiguration : IEntityTypeConfiguration<SaleOrderItem>
    {
        public void Configure(EntityTypeBuilder<SaleOrderItem> builder)
        {
            builder.ToTable("SaleOrderItems");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.SystemDescription)
                .HasConversion(description => description == null ? null : description.Value,
                    value => string.IsNullOrWhiteSpace(value) ? null : Description.Create(value))
                .IsRequired(false);

            builder.Property(u => u.SellerDescription)
                .HasConversion(description => description == null ? null : description.Value,
                    value => string.IsNullOrWhiteSpace(value) ? null : Description.Create(value))
                .IsRequired(false);

            builder.Property(u => u.ModifiedByAdmin)
                .IsRequired();

            builder.HasOne(u => u.SubCategory)
                .WithMany()
                .HasForeignKey(u => u.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(s => s.Images)
                .HasConversion(
                    v => string.Join(",", v),
                    v => string.IsNullOrWhiteSpace(v)
                        ? new List<Guid>()
                        : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
                )
                .HasColumnType("nvarchar(max)");
        }
    }
}
