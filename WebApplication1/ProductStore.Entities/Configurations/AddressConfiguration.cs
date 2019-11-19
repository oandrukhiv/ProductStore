using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Entities.Models;

namespace ProductStore.Entities.Configurations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.ApartmentNumber)
                .HasMaxLength(50);
            builder.Property(e => e.BuildingNumber)
                .HasMaxLength(50);
            builder.Property(e => e.Details)
                .HasMaxLength(50);
            builder.Property(e => e.Street)
                .HasMaxLength(50);
            builder.HasOne(x => x.User)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.UserId);
            builder.HasOne(x => x.City)
                .WithMany(u => u.Addresses)
                .HasForeignKey(a => a.CityId);
            builder.HasMany(e => e.Orders)
                .WithOne(x => x.Address)
                .HasForeignKey(x => x.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
            SeedData(builder);
        }
        private static void SeedData(EntityTypeBuilder<Address> entity)
        {
            entity.HasData(
                new Address 
                { 
                    Id = 1, 
                    Street = "street 2", 
                    Details = "details2", 
                    BuildingNumber = 555, 
                    ApartmentNumber = 999 ,
                    UserId = 1,
                    CityId = 2
                });
        }
    }
}
