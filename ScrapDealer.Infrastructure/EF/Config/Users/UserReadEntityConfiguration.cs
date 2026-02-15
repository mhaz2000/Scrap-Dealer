using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Users
{

    internal sealed class UserReadEntityConfiguration : IEntityTypeConfiguration<UserReadModel>, IEntityTypeConfiguration<RoleReadModel>,
        IEntityTypeConfiguration<UserRoleReadModel>
    {
        public void Configure(EntityTypeBuilder<RoleReadModel> builder)
        {
            builder.ToTable("Roles");

            builder.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(r => r.RolePermissions)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public void Configure(EntityTypeBuilder<UserReadModel> builder)
        {
            builder.ToTable("Users");

            builder.HasQueryFilter(p => !p.IsDeleted);

            builder.HasMany(u => u.UserRoles)
            .WithOne(ur => ur.User)
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        }

        public void Configure(EntityTypeBuilder<UserRoleReadModel> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(b => b.Id);

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }

}
