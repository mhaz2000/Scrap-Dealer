using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;

namespace ScrapDealer.Infrastructure.EF.Config.Invoices;

internal class InvoiceWriteEntityConfiguration : IEntityTypeConfiguration<Invoice>, IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("Invoices");
        builder.HasKey(x => x.Id);

        builder.Property(u => u.Amount)
           .HasConversion(amount => amount.Value, amount => Amount.Create(amount))
           .IsRequired();

        builder.HasOne(x => x.Contract)
            .WithMany()
            .HasForeignKey(x => x.ContractId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasQueryFilter(p => !p.IsDeleted);
    }

    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("InvoiceItems");
        builder.HasKey(x => x.Id);

        builder.Property(u => u.Amount)
   .HasConversion(amount => amount.Value, amount => Amount.Create(amount))
   .IsRequired();

        builder.HasOne(u => u.SubCategory)
            .WithMany()
            .HasForeignKey(u => u.SubCategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
