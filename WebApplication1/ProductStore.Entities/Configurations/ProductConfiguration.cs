using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Entities.Models;

namespace ProductStore.Entities.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Price)
                .IsRequired();           

            SeedData(builder);
        }

        private static void SeedData(EntityTypeBuilder<Product> entity)
        {
            entity.HasData(
                new Product { Id = 1, Name = "product1", Price = 10 },
                new Product { Id = 2, Name = "product2", Price = 20}
                );
        }
    }
}
