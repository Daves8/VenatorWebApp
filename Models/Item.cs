using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.Models
{
    public class Item : Entity
    {
        public Item() { }
        public Item(int id) => Id = id;

        public string Description { get; set; }
        public ItemCategory Category { get; set; }
        public double Price { get; set; }
        public bool IsHidden { get; set; }
        public string ImageUrl { get; set; } // byte[]?

        public override bool IsValid() => true;
    }
}
