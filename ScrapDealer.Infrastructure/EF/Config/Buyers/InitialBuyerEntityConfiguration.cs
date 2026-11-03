using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.Entities;
using ScrapDealer.Domain.ValueObjects.Profiles;
using ScrapDealer.Domain.ValueObjects.Users;
using ScrapDealer.Infrastructure.EF.Models;

namespace ScrapDealer.Infrastructure.EF.Config.Buyers;

internal sealed class InitialBuyerWriteEntityConfiguration : IEntityTypeConfiguration<InitialBuyer>
{
    public void Configure(EntityTypeBuilder<InitialBuyer> builder)
    {
        builder.ToTable("InitialBuyers");
        builder.HasKey(x => x.Id);
        builder.OwnsOne(x => x.PersonName, name =>
        {
            name.Property(x => x.FirstName).HasColumnName("FirstName").IsRequired();
            name.Property(x => x.LastName).HasColumnName("LastName").IsRequired();
        });
        builder.Property(x => x.Phone)
            .HasConversion(phone => phone.Value, value => Phone.Create(value))
            .HasColumnName("Phone").IsRequired();
        builder.HasIndex(x => x.Phone).IsUnique();
        builder.Property(x => x.Area).IsRequired(false);
    }
}

internal sealed class InitialBuyerReadEntityConfiguration : IEntityTypeConfiguration<InitialBuyerReadModel>
{
    public void Configure(EntityTypeBuilder<InitialBuyerReadModel> builder)
    {
        builder.ToTable("InitialBuyers");
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Phone).IsUnique();
    }
}
