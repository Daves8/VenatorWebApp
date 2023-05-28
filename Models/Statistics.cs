using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class Statistics : Entity
    {
        public int ItemsCount { get; set; }
        public double GoldSpent { get; set; }
        public int CompletedQuestsNumber { get; set; }
    }
}
