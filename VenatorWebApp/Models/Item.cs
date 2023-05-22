using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.Models
{
    public class Item : Entity
    {
        public string Description { get; set; }
        public ItemCategory Category { get; set; }
        public double Price { get; set; }
        public bool IsHidden { get; set; }
        public string ImageUrl { get; set; } // byte[]?
    }
}
