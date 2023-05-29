using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.Models
{
    public class User : Login
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ImageUrl { get; set; } // byte[]?
        public Role Role { get; set; }
        public double? GoldAmount { get; set; }

        public override string? ToString()
        {
            return base.ToString() + $", FullName={FullName}, Email={Email}, PhoneNumber={PhoneNumber}, ImageUrl={ImageUrl}, Role={Role}, GoldAmount={GoldAmount}";
        }

        public override bool IsValid() => base.IsValid() && !string.IsNullOrEmpty(Email);
    }
}
