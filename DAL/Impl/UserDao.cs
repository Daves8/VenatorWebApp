using Dapper;
using VenatorWebApp.DAL.Base;
using VenatorWebApp.DAL.Mapper;
using VenatorWebApp.Models;

namespace VenatorWebApp.DAL.Impl
{
    public class UserDao : BaseDao, IUserDao
    {
        public UserDao(IConfiguration config) : base(config)
        {
            SqlMapper.SetTypeMap(typeof(User), CustomMapper.GetUserMapper());
            SqlMapper.SetTypeMap(typeof(Statistics), CustomMapper.GetStatisticsMapper());
        }

        public void CreateUser(User user)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                USER_NAME = user.Name,
                PASSWORD = user.Password,
                FULL_NAME = user.FullName,
                EMAIL = user.Email,
                PHONE_NUMBER = user.PhoneNumber,
                IMAGE_URL = user.ImageUrl
            };
            connection.Execute("DBO.CREATE_USER", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteUser(User user)
        {
            using var connection = GetConnection();
            connection.Execute("DBO.DELETE_USER", new { ID = user.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<User> QueryAllUsers()
        {
            using var connection = GetConnection();
            return connection.Query<User>("DBO.QUERY_USERS", commandType: System.Data.CommandType.StoredProcedure);
        }

        public User QueryUser(int id)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<User>("DBO.QUERY_USER_BY_ID", new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public User QueryUserByEmail(string email)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<User>("DBO.QUERY_USER_BY_EMAIL", new { EMAIL = email }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public User QueryUserByUsername(string username)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<User>("DBO.QUERY_USER_BY_USER_NAME", new { USER_NAME = username }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public Statistics QueryUserStatistics(User user)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<Statistics>("DBO.QUERY_USER_STATISTICS", new { ID = user.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void UpdateUser(User user)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                ID = user.Id,
                USER_NAME = user.Name,
                PASSWORD = user.Password,
                FULL_NAME = user.FullName,
                EMAIL = user.Email,
                PHONE_NUMBER = user.PhoneNumber,
                IMAGE_URL = user.ImageUrl,
                ROLE = user.Role,
                GOLD_AMOUNT = user.GoldAmount
            };
            connection.Execute("DBO.UPDATE_USER", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
