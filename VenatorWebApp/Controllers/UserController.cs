using Microsoft.AspNetCore.Mvc;
using Dapper;
using VenatorWebApp.Models;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using VenatorWebApp.DAL.Mapper;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }



        [HttpGet("get")]
        public A Get2() => new A("Test get2 OK1");


        [HttpGet]
        public string Get() => "Test OK!";

        [Route("getUsersString")]
        [HttpGet]
        public string GetUsersString()
        {

            _logger.Log(LogLevel.Information, "call GetUsersString");

            string result = "";

            using (SqlConnection connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                using (SqlCommand command = new SqlCommand("dbo.GetUsers", connection))
                {
                    // указание типа команды
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // открытие подключения к базе данных
                    connection.Open();

                    // выполнение команды и получение результата в виде объекта SqlDataReader
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // обработка результата выполнения команды
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0); // получение значения первого столбца
                            string userName = reader.GetString(1); // получение значения второго столбца
                            string password = reader.GetString(2); // получение значения третьего столбца
                            string fullName = reader.GetString(3); // получение значения четвертого столбца

                            // обработка полученных данных
                            // ...
                            result += id + " " + userName + " " + password + " " + fullName + " " + DateTime.Now + "\n";
                        }
                    }
                }
            }

            return result;
        }

        [Route("getUsers")]
        [HttpGet]
        public IEnumerable<User> GetUsers()
        {

            _logger.Log(LogLevel.Information, "call GetUsers");

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                var users = connection.Query<User>("dbo.GetUsers", commandType: System.Data.CommandType.StoredProcedure);
                return users;
            }

        }
        
        [Route("getUser/{id}")]
        [HttpGet]
        public User GetUser(int id)
        {
            _logger.Log(LogLevel.Information, "call GetUser with id = {userId}", id);
            _logger.Log(LogLevel.Trace, "trace call GetUser with id = {userId}", id);

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                SqlMapper.SetTypeMap(typeof(User), CustomMapper.GetMapperByType(typeof(User)));

                var parameters = new { Id = id };

                var user = connection.QueryFirstOrDefault<User>("dbo.Get_User_By_Id", parameters, commandType: System.Data.CommandType.StoredProcedure);
                return user;
            }
        }


        [HttpGet]
        [Route("auth1")]
        [Authorize]
        public string GetProtectedData1() => "ДАА!!! " + DateTime.Now;

        [HttpGet]
        [Route("user")]
        [Authorize(Policy = AuthPolicy.USER_REQUIRE)]
        public string GetProtectedData3() => "ДАА!!! " + DateTime.Now;

        [HttpGet]
        [Route("mod")]
        [Authorize(Policy = AuthPolicy.MODERATOR_REQUIRE)]
        public string GetProtectedData3534() => "ДАА!!! " + DateTime.Now;

        [HttpGet]
        [Route("adm")]
        [Authorize(Policy = AuthPolicy.ADMINISTRATOR_REQUIRE)]
        public string GetProtectedData4533() => "ДАА!!! " + DateTime.Now;
    }
}
