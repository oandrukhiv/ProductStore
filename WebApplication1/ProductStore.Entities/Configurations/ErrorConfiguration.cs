using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Entities.Models;

namespace ProductStore.Entities.Configurations
{
    internal class ErrorConfiguration : IEntityTypeConfiguration<ErrorModel>
    {
        public void Configure(EntityTypeBuilder<ErrorModel> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(3);
            builder.Property(e => e.Response)
                .IsRequired();
            builder.Property(e => e.When)
                .IsRequired();

            SeedData(builder);
        }
        private static void SeedData(EntityTypeBuilder<ErrorModel> entity)
        {
            entity.HasData(
                new ErrorModel { Id = 1, When = DateTime.Now, Response = "Default", Code = 200}
            );
        }
    }
}
