using System.Collections.Generic;

namespace ProductStore.Entities.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Address> Addresses { get; set; }

    }
}
