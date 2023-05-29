using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class News : Textual
    {
        public override bool IsValid() => !string.IsNullOrEmpty(Text);
    }
}
