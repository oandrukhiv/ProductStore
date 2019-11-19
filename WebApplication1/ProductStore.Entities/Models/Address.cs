using System.Collections.Generic;

namespace ProductStore.Entities.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int BuildingNumber { get; set; }
        public int ApartmentNumber { get; set; }
        public string Details { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
