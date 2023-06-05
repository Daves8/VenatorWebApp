using VenatorWebApp.Models.Abstracts.Base;

namespace VenatorWebApp.Models.Abstracts
{
    //TODO: make abstract
    public class Textual : BaseEntity
    {
        public Textual() { }

        public Textual(int id) : base(id) { }

        public string? Text { get; set; }
        public Textual? Parent { get; set; }
        public string? Metrics { get; set; }
        public int LikesCount { get; set; }
        public int DislikesCount { get; set; }
        public bool CurrentLike { get; set; }
        public bool CurrentDislike { get; set; }
        public bool IsHidden { get; set; }

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
