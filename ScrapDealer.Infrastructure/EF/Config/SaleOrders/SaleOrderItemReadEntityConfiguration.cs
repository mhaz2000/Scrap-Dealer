using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.SaleOrders
{
    internal class SaleOrderItemReadEntityConfiguration : IEntityTypeConfiguration<SaleOrderItemReadModel>
    {
        public void Configure(EntityTypeBuilder<SaleOrderItemReadModel> builder)
        {
            builder.ToTable("SaleOrderItems");
            builder.HasKey(x => x.Id);

            builder.HasOne(u => u.SubCategory)
                .WithMany()
                .HasForeignKey(u => u.SubCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(u => u.ModifiedByAdmin)
                .IsRequired();

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
