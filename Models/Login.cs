using VenatorWebApp.Models.Abstracts;

namespace VenatorWebApp.Models
{
    public class Login : Entity
    {
        public string? Password { get; set; }

        public override string? ToString()
        {
            return base.ToString() + $", Password={Password}";
        }

        public bool IsValid0()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Password);
        }
    }
}
