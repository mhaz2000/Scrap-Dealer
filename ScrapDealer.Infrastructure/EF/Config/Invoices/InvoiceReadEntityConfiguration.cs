using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Invoices;

internal class InvoiceReadEntityConfiguration : IEntityTypeConfiguration<InvoiceReadModel>, IEntityTypeConfiguration<InvoiceItemReadModel>
{
    public void Configure(EntityTypeBuilder<InvoiceReadModel> builder)
    {
        builder.ToTable("Invoices");
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Contract)
            .WithMany()
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasMany(s => s.Items)
            .WithOne(t=> t.Invoice)
            .HasForeignKey(t => t.InvoiceId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }

    public void Configure(EntityTypeBuilder<InvoiceItemReadModel> builder)
    {
        builder.ToTable("InvoiceItems");
        builder.HasKey(x => x.Id);

        builder.HasOne(u => u.SubCategory)
            .WithMany()
            .HasForeignKey(u => u.SubCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}