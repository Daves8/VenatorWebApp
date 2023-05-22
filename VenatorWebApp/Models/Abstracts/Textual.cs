using VenatorWebApp.Models.Abstracts.Base;

namespace VenatorWebApp.Models.Abstracts
{
    public abstract class Textual : BaseEntity
    {
        public string Text { get; set; }
        public Textual Parent { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public bool IsHidden { get; set; }
    }
}
