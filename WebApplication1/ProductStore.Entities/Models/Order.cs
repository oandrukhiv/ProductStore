using System;
using System.Collections.Generic;

namespace ProductStore.Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public ICollection<Product> Products { get; set; }
        public string AdditionalDetails { get; set; }
        public double TotalPrice { get; set; }
        public bool IsPaid { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
    }
}
