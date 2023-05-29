using VenatorWebApp.DAL;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Services.Exceptions;

namespace VenatorWebApp.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserDao _userDao;
        private readonly IItemDao _itemDao;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserDao userDao, IItemDao itemDao, ILogger<UserService> logger)
        {
            _userDao = userDao;
            _itemDao = itemDao;
            _logger = logger;
        }

        public void CreateUser(User user)
        {
            if (!user.IsValid())
            {
                throw new HttpResponseException("Не введені усі необхідні дані", 409);
            }

            User queriedUserByUsername = GetUserByUsername(user.Name);
            User queriedUserByEmail = GetUserByEmail(user.Email);

            if (queriedUserByUsername != null || queriedUserByEmail != null)
            {
                string field = queriedUserByUsername != null ? "таким логіном" : "такою електронною адресою";
                throw new HttpResponseException("Користувач з " + field + " вже існує", 409);
            }

            _userDao.CreateUser(user);
        }

        public void DeleteUser(User user) => _userDao.DeleteUser(user);

        public IEnumerable<User> GetAllUsers() => _userDao.QueryAllUsers();

        public User GetUser(int id) => _userDao.QueryUser(id);

        public User GetUserByEmail(string email) => _userDao.QueryUserByEmail(email);

        public User GetUserByUsername(string username) => _userDao.QueryUserByUsername(username);

        public Statistics GetUserStatistics(User user)
        {
            Statistics statistics = _userDao.QueryUserStatistics(user);

            statistics.TotalKilled = statistics.KilledPlayersCounter + statistics.KilledNpcCounter + statistics.KilledAnimalsCounter;
            statistics.TotalDeath = statistics.DeathFromPlayersCounter + statistics.DeathFromNpcCounter + statistics.DeathFromAnimalsCounter;

            IEnumerable<Item> itemsInUser = _itemDao.QueryAllItemsInUser(user, ItemIn.Inventory);
            statistics.GoldSpent = itemsInUser?.Sum(item => item.Price) ?? 0;
            statistics.TotalItems = itemsInUser?.Count() ?? 0;

            return statistics;
        }

        public void UpdateUser(User user)
        {
            if (!user.IsValid())
            {
                throw new HttpResponseException("Не введені усі необхідні дані", 409);
            }
            _userDao.UpdateUser(user);
        }
    }
}
