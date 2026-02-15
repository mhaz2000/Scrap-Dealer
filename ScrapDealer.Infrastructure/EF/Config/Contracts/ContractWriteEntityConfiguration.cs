using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapDealer.Infrastructure.EF.Config.Contracts
{

    public class ContractWriteEntityConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.ToTable("Contracts");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.CommissionAmount)
                .HasConversion(amount => amount.Value, amount => Amount.Create(amount))
                .IsRequired();

            builder.Property(u => u.Amount)
                .HasConversion(amount => amount.Value, amount => Amount.Create(amount))
                .IsRequired();

            builder.HasOne(u => u.SaleOrder)
                .WithMany()
                .HasForeignKey(u => u.SaleOrderId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
