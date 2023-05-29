using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class Comment : Textual
    {
        public override bool IsValid() => !string.IsNullOrEmpty(Text);
    }
}
