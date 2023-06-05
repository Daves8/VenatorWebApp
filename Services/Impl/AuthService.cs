using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Services.Exceptions;

namespace VenatorWebApp.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserService userService, IHttpContextAccessor httpContextAccessor, IConfiguration config, ILogger<AuthService> logger)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
            _logger = logger;
        }

        public User GetCurrentUser()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string idStr = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!string.IsNullOrEmpty(idStr))
                {
                    return _userService.GetUser(int.Parse(idStr));
                }
            }
            return null;
        }

        public Role GetUserRole()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string roleValue = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                if (!string.IsNullOrEmpty(roleValue))
                {
                    return (Role)Enum.Parse(typeof(Role), roleValue);
                }
            }
            return Role.User;
        }

        public (User user, string token) Login(Login login)
        {
            _logger.Log(LogLevel.Trace, $"call #Login, login = {login}");

            if (!login.IsValid())
            {
                throw new HttpResponseException("Не введені усі необхідні дані", 409);
            }

            User user = _userService.GetUserByUsername(login.Name);

            if (user == null || user.Password != login.Password)
            {
                throw new HttpResponseException("Логін або пароль не вірні", 409);
            }

            return Authorize(user);
        }

        public (User user, string token) Register(User user)
        {
            _logger.Log(LogLevel.Trace, $"call #Register, user = {user}");

            _userService.CreateUser(user);
            return Authorize(_userService.GetUserByUsername(user.Name));
        }

        protected (User user, string token) Authorize(User user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var tokenOptions = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(double.Parse(_config["Jwt:TokenExpirationDays"])),
                signingCredentials: signinCredentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return (user, tokenString);
        }
    }
}
