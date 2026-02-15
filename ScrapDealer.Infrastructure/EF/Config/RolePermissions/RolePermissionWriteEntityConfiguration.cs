using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Roles;

namespace ScrapDealer.Infrastructure.EF.Config.RolePermissions
{
    internal class RolePermissionWriteEntityConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.PermissionName)
                .HasConversion(name => name.Value, name => PermissionName.Create(name))
                .IsRequired();


            builder.HasOne(u => u.Role)
                .WithMany(r=> r.RolePermissions)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
