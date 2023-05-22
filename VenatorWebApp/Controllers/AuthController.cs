using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VenatorWebApp.DAL.Mapper;
using VenatorWebApp.Models;

namespace VenatorWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly ILogger<UserController> _logger;

        public AuthController(IConfiguration config, ILogger<UserController> logger)
        {
            _config = config;
            _logger = logger;
        }

        [HttpPost("login")]
        //[AllowAnonymous] // using Microsoft.AspNetCore.Authorization;
        public IActionResult Login(Login login)
        {
            if (login == null || !login.IsValid0())
            {
                return BadRequest("User is in invalid state");
            }

            _logger.LogTrace($"call #Login, login = {login}");

            User user;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                SqlMapper.SetTypeMap(typeof(User), CustomMapper.GetUserMapper());
                user = connection.QueryFirstOrDefault<User>("[DBO].[CHECK_USER_CREDENTIALS]", new { user_name = login.Name, password = login.Password },
                    commandType: System.Data.CommandType.StoredProcedure);
            }

            if (user != null)
            {
                return Authorize(user);
            }
            else
            {
                return Unauthorized("Such user is not found");
            }
        }

        [HttpPost("registration")]
        public IActionResult Registration(User user)
        {
            if (user == null || !user.IsValid0())
            {
                return BadRequest("User is in invalid state");
            }

            _logger.LogTrace($"call #Registration, user = {user}");

            bool isUserAlreadyExists;
            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {
                isUserAlreadyExists = connection.QueryFirstOrDefault<bool>("[DBO].[CHECK_USER_EXISTENCE]", new { user_name = user.Name, email = user.Email },
                    commandType: System.Data.CommandType.StoredProcedure);
            }

            if (!isUserAlreadyExists)
            {
                return Authorize(CreateUser(user));
            }
            else
            {
                return Conflict("User already exist");
            }
        }

        public IActionResult Authorize(User user)
        {

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>
                {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
                };

            var tokeOptions = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(double.Parse(_config["Jwt:TokenExpirationDays"])),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return Ok(new { Token = tokenString, user = user });
        }

        public User CreateUser(User user)
        {

            using (var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection")))
            {

                var parameters = new
                {
                    USER_NAME = user.Name,
                    PASSWORD = user.Password,
                    FULL_NAME = user.FullName,
                    EMAIL = user.Email,
                    PHONE_NUMBER = user.PhoneNumber,
                    IMAGE_URL = user.ImageUrl,
                    //ROLE = user.Role,
                    //GOLD_AMOUNT = user.GoldAmount
                };
                connection.Execute("DBO.CREATE_USER", parameters, commandType: System.Data.CommandType.StoredProcedure);

                SqlMapper.SetTypeMap(typeof(User), CustomMapper.GetMapperByType(typeof(User)));
                return connection.QueryFirstOrDefault<User>("[DBO].[GET_USER_BY_USER_NAME]", new { user_name = user.Name}, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        [HttpGet("checkToken")]
        [Authorize]
        public IActionResult checkToken() => Ok();
    }
}