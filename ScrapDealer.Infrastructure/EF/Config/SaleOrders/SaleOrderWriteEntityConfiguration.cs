using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;
using ScrapDealer.Domain.ValueObjects.SaleOrders;

namespace ScrapDealer.Infrastructure.EF.Config.SaleOrders
{
    internal class SaleOrderWriteEntityConfiguration : IEntityTypeConfiguration<SaleOrder>
    {
        public void Configure(EntityTypeBuilder<SaleOrder> builder)
        {
            builder.ToTable("SaleOrders");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Code)
                .HasConversion(code => code.Value, code => Code.Create(code))
                .IsRequired();

            builder.Property(u => u.Address)
                .HasConversion(address => address.Value, address => SaleOrderAddress.Create(address))
                .IsRequired();

            builder.Property(u => u.Telephone)
                .HasConversion(telephone => telephone == null ? string.Empty : telephone.Value, telephone => Telephone.Create(telephone))
                .IsRequired();

            builder.OwnsOne(x => x.Location, location =>
            {
                location.Property(a => a.Latitude)
                    .IsRequired()
                    .HasColumnName("Latitude");

                location.Property(a => a.Longitude)
                    .IsRequired()
                    .HasColumnName("Longitude");
            });

            builder.HasOne(u => u.Seller)
                .WithMany()
                .HasForeignKey(u => u.SellerId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
