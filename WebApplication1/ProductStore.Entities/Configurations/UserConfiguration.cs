using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Entities.Models;
using System;
using System.Collections.Generic;

namespace ProductStore.Entities.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);                
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);           
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.CellNumber)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.Birthday)                
                .HasMaxLength(50);
            builder.Property(e => e.Role)
                .IsRequired();

            builder.HasMany(e => e.Addresses)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(e => e.Orders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);


            SeedData(builder);
        }
        private static void SeedData(EntityTypeBuilder<User> entity)
        {
            _ = entity.HasData(
                new User
                {
                    Id = 1,
                    FirstName = "Vasya",
                    LastName = "Pupkin",
                    Password = "123",
                    Birthday = Convert.ToDateTime("12OCT1988"),
                    CellNumber = "888",
                    Email = "oa@gmail.com",
                    Role = "user"
                });
        }
    }
}
