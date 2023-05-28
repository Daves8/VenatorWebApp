using VenatorWebApp.Models;

namespace VenatorWebApp.Services
{
    public interface IUserService
    {
        User GetUser(int id);
        User GetUserByUsername(string username);
        User GetUserByEmail(string username);
        IEnumerable<User> GetAllUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Statistics GetUserStatistics(User user);
    }
}