using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.RolePermissions
{
    internal class RolePermissionReadEntityConfiguration : IEntityTypeConfiguration<RolePermissionReadModel>
    {
        public void Configure(EntityTypeBuilder<RolePermissionReadModel> builder)
        {
            builder.ToTable("RolePermissions");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
