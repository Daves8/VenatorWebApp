using VenatorWebApp.Models;

namespace VenatorWebApp.DAL
{
    public interface IUserDao
    {
        User QueryUser(int id);
        User QueryUserByUsername(string username);
        IEnumerable<User> QueryAllUsers();
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
