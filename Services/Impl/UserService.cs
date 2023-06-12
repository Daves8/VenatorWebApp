using VenatorWebApp.DAL;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Services.Base;
using VenatorWebApp.Services.Exceptions;
using VenatorWebApp.Services.Util;

namespace VenatorWebApp.Services.Impl
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserDao _userDao;
        private readonly IItemDao _itemDao;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserDao userDao, IItemDao itemDao, IFillModelsService fillModelsService, ILogger<UserService> logger) : base(fillModelsService)
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
            //TODO: rework without query
            _userDao.InitUserStatistics(GetUserByUsername(user.Name));
        }

        public void DeleteUser(User user) => _userDao.DeleteUser(user);

        public IEnumerable<User> GetAllUsers() => _userDao.QueryAllUsers().ToList().Select(o => { return Fill(o); });

        public User GetUser(int id) => Fill(_userDao.QueryUser(id));

        public User GetUserByEmail(string email) => Fill(_userDao.QueryUserByEmail(email));

        public User GetUserByUsername(string username) => Fill(_userDao.QueryUserByUsername(username));

        public Statistics GetUserStatistics(User user)
        {
            Statistics statistics = _userDao.QueryUserStatistics(user);

            statistics.TotalKilled = statistics.KilledPlayersCounter + statistics.KilledNpcCounter + statistics.KilledAnimalsCounter;
            statistics.TotalDeath = statistics.DeathFromPlayersCounter + statistics.DeathFromNpcCounter + statistics.DeathFromAnimalsCounter;

            IEnumerable<Item> itemsInUser = _itemDao.QueryAllItemsInUser(user, ItemIn.Inventory);
            statistics.GoldSpent = itemsInUser?.Sum(item => item.Price) ?? 0;
            statistics.TotalItems = itemsInUser?.Count() ?? 0;

            return Fill(statistics);
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
