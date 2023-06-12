using VenatorWebApp.Models;

namespace VenatorWebApp.DAL
{
    public interface IUserDao
    {
        User QueryUser(int id);
        User QueryUserByUsername(string username);
        User QueryUserByEmail(string email);
        IEnumerable<User> QueryAllUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
        Statistics QueryUserStatistics(User user);
        void InitUserStatistics(User user);
    }
}
