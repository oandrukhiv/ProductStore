using System;
using System.Collections.Generic;

namespace ProductStore.Entities.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public DateTime Birthday { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
        public string Role { get; set; }
        public City City { get; set; }
    }
}
