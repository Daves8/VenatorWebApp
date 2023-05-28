using VenatorWebApp.DAL;
using VenatorWebApp.Models;

namespace VenatorWebApp.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserDao _userDao;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserDao userDao, ILogger<UserService> logger)
        {
            _userDao = userDao;
            _logger = logger;
        }

        public void CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUserByEmail(string username)
        {
            throw new NotImplementedException();
        }

        public User GetUserByUsername(string username)
        {
            throw new NotImplementedException();
        }
        
        public Statistics GetUserStatistics(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
