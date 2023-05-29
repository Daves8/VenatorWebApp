using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class Statistics : Entity
    {
        public int TotalItems { get; set; }
        public double GoldSpent { get; set; }
        public int CompletedQuestsCounter { get; set; }
        public int TotalKilled { get; set; }
        public int KilledPlayersCounter { get; set; }
        public int KilledNpcCounter { get; set; }
        public int KilledAnimalsCounter { get; set; }
        public int TotalDeath { get; set; }
        public int DeathFromPlayersCounter { get; set; }
        public int DeathFromNpcCounter { get; set; }
        public int DeathFromAnimalsCounter { get; set; }

        public override bool IsValid() => true;
    }
}
