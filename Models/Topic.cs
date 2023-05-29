using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class Topic : Textual
    {
        public override bool IsValid() => !string.IsNullOrEmpty(Text);
    }
}
