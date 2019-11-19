using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductStore.Entities.Models;
using System;

namespace ProductStore.Entities.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.AdditionalDetails)
                .HasMaxLength(50);
            builder.Property(e => e.CreationDate)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(e => e.IsPaid)
                .IsRequired()
                .HasMaxLength(50);           
            builder.Property(e => e.TotalPrice)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasOne(x => x.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(a => a.UserId);
            builder.HasOne(x => x.Address)
                .WithMany(u => u.Orders)
                .HasForeignKey(a => a.AddressId);

            SeedData(builder);
        }
        private static void SeedData(EntityTypeBuilder<Order> entity)
        {            
            entity.HasData(
                new Order { 
                    Id = 1,  
                    TotalPrice = 88.88, 
                    CreationDate = Convert.ToDateTime("12OCT1988"), 
                    IsPaid = true, 
                    AdditionalDetails = "details", 
                    UserId = 1, 
                    AddressId = 1 });
        }
    }
}
