using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class Login : Entity
    {
        public string? Password { get; set; }

        public override string? ToString()
        {
            return base.ToString() + $", Password={!string.IsNullOrEmpty(Password)}";
        }

        public override bool IsValid() => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password);
    }
}
