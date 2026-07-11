using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Consts;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Domain.ValueObjects.Roles;
using ScrapDealer.Domain.ValueObjects.Users;

namespace ScrapDealer.Infrastructure.EF.Config.Users
{
    internal sealed class UserWriteEntityConfiguration : IEntityTypeConfiguration<User>, IEntityTypeConfiguration<Role>, IEntityTypeConfiguration<UserRole>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Username)
                .HasConversion(username => username.Value, username => Username.Create(username))
                .IsRequired();

            builder.OwnsOne(x => x.PersonName, name =>
            {
                name.Property(a => a.FirstName)
                    .IsRequired()
                    .HasColumnName("FirstName");

                name.Property(a => a.LastName)
                    .IsRequired()
                    .HasColumnName("LastName");
            });

            builder.Property(u => u.PasswordHash)
                .HasConversion(hash => hash != null ? hash.Value : (string?)null,
                    value => value != null ? PasswordHash.FromHash(value) : null);

            builder.Property(u => u.Phone)
                .HasConversion(phone => phone.Value, phone => Phone.Create(phone))
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired();

            builder.Property(u => u.ReferralCode)
                .HasConversion(
                    code => code == null ? (string?)null : code.Value,
                    value => value == null ? null : ReferralCode.Create(value))
                .IsRequired(false);

            builder.HasIndex(u => u.ReferralCode)
                .IsUnique();

            builder.HasMany(u => u.Roles)
                .WithOne()
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasConversion(name => name.Value, name => RoleName.Create(name))
                .IsRequired();

            builder.HasMany(r => r.RolePermissions)
                .WithOne()
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(r => r.UserRoles)
            .WithOne()
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Roles");
        }

        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasKey(ur => ur.Id);

            builder.HasOne(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("UserRoles");
        }
    }

}
