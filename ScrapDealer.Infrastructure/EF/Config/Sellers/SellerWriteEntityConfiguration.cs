using ScrapDealer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScrapDealer.Domain.ValueObjects.Profiles;

namespace ScrapDealer.Infrastructure.EF.Config.Buyers
{
    internal class SellerWriteEntityConfiguration : IEntityTypeConfiguration<Seller>
    {
        public void Configure(EntityTypeBuilder<Seller> builder)
        {
            builder.ToTable("Sellers");
            builder.HasKey(x => x.Id);

            builder.Property(u => u.Email)
                .HasConversion(email => email.Value, email => Email.Create(email))
                .IsRequired();

            builder.Property(u => u.Score)
                .HasConversion(score => score.Value, score => Score.Create(score))
                .IsRequired();

            builder.Property(u => u.NationalCode)
                .HasConversion(nationalCode => nationalCode.Value, nationalCode => NationalCode.Create(nationalCode))
                .IsRequired();

            builder.HasOne(u => u.User)
                .WithMany()
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.OwnsOne(x => x.Address, address =>
            {
                address.Property(a => a.Province)
                    .IsRequired()
                    .HasColumnName("Province");

                address.Property(a => a.City)
                    .IsRequired()
                    .HasColumnName("City");

                address.Property(a => a.PostalCode)
                    .IsRequired()
                    .HasColumnName("PostalCode");

                address.Property(a => a.Description)
                    .IsRequired()
                    .HasColumnName("AddressDescription");

                address.Property(a => a.ActivityArea)
                    .HasColumnName("ActivityArea").IsRequired(false);
            });

            builder.OwnsOne(x => x.PersonName, address =>
            {
                address.Property(a => a.FirstName)
                    .IsRequired()
                    .HasColumnName("FirstName");

                address.Property(a => a.LastName)
                    .IsRequired()
                    .HasColumnName("LastName");
            });


            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
