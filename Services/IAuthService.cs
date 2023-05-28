using VenatorWebApp.Models;

namespace VenatorWebApp.Services
{
    public interface IAuthService
    {
        User Login(Login login);
        User Register(User user);
        User Authorize(User user);
    }
}
