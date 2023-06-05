using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class News : Textual
    {
        public News() { }

        public News(int id) : base(id) { }

        public override bool IsValid() => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Text);
    }
}
