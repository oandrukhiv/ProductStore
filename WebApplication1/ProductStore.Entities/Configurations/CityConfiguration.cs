using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Entities.Models;

namespace ProductStore.Entities.Configurations
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasMany(e => e.Addresses)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<City> entity)
        {
            entity.HasData(
                new City { Id = 1, Name = "Lviv" },
                new City { Id = 2, Name = "Kyiv" }
                );
        }
    }
}
