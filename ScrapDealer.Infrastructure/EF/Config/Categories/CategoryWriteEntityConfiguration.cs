using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Category;

namespace ScrapDealer.Infrastructure.EF.Config.Categories
{
    internal class CategoryWriteEntityConfiguration : IEntityTypeConfiguration<Category>, IEntityTypeConfiguration<SubCategory>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Name)
                .HasConversion(category => category.Value, category => CategoryName.Create(category))
                .IsRequired();

            builder.HasMany(u => u.SubCategories)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryId);

            builder.OwnsOne(x => x.PriceRange, pr =>
            {
                pr.Property(p => p.MinValue)
                    .HasColumnName("MinPrice")
                    .IsRequired();

                pr.Property(p => p.MaxValue)
                    .HasColumnName("MaxPrice")
                    .IsRequired();
            });

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.Property(s => s.Images)
            .HasConversion(
                v => string.Join(",", v),
                v => string.IsNullOrWhiteSpace(v)
                    ? new List<Guid>()
                    : v.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(Guid.Parse).ToList()
            )
            .HasColumnType("nvarchar(max)");
        }

        public void Configure(EntityTypeBuilder<SubCategory> builder)
        {
            builder.ToTable("SubCategories");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Name)
                .HasConversion(category => category.Value, category => CategoryName.Create(category))
                .IsRequired();

            builder.OwnsOne(x => x.PriceRange, pr =>
            {
                pr.Property(p => p.MinValue)
                    .HasColumnName("MinPrice")
                    .IsRequired();

                pr.Property(p => p.MaxValue)
                    .HasColumnName("MaxPrice")
                    .IsRequired();
            });

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

            builder.HasQueryFilter(p => !p.IsDeleted);

        }
    }
}
