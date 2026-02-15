using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Categories
{
    internal class CategoryReadEntityConfiguration : IEntityTypeConfiguration<CategoryReadModel>, IEntityTypeConfiguration<SubCategoryReadModel>
    {
        public void Configure(EntityTypeBuilder<CategoryReadModel> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);

            builder.Property(s => s.Images)
            .HasConversion(
                v => string.Join(",", v),
                v => string.IsNullOrWhiteSpace(v)
                    ? new List<Guid>()
                    : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
            )
            .HasColumnType("nvarchar(max)");

            builder.HasMany(u => u.SubCategories)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }

        public void Configure(EntityTypeBuilder<SubCategoryReadModel> builder)
        {
            builder.ToTable("SubCategories");
            builder.HasKey(x => x.Id);

            builder.HasOne(u => u.Category)
                .WithMany(c => c.SubCategories)
                .HasForeignKey(c => c.CategoryId)
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
