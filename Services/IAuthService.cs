using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.Services
{
    public interface IAuthService
    {
        (User user, string token) Login(Login login);
        (User user, string token) Register(User user);
        Role GetUserRole();
        User GetCurrentUser();
    }
}
