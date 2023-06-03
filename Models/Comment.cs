using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.Models
{
    public class Comment : Textual
    {
        //TODO: rework
        public TextualType ParentType { get; set; }
        //TODO: rework
        public int ParentId { get; set; }

        public override bool IsValid() => !string.IsNullOrEmpty(Text);
    }
}
