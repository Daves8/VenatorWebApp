using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class Message : Textual
    {
        public User ToUser { get; set; }
    }
}
