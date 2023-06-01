using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class Message : Textual
    {
        public User ToUser { get; set; }

        //TODO: rework
        public int ToUserId { get; set; }

        public override bool IsValid() => !string.IsNullOrEmpty(Text);
    }
}
