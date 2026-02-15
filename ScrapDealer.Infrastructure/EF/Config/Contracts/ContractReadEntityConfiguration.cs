using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Contracts
{
    internal class ContractReadEntityConfiguration : IEntityTypeConfiguration<ContractReadModel>
    {
        public void Configure(EntityTypeBuilder<ContractReadModel> builder)
        {
            builder.ToTable("Contracts");

            builder.HasOne(u => u.SaleOrder)
                .WithMany()
                .HasForeignKey(u => u.SaleOrderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
