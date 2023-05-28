using VenatorWebApp.Models;

namespace VenatorWebApp.Services.Impl
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserService userService, ILogger<AuthService> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public User Authorize(User user)
        {
            throw new NotImplementedException();
        }

        public User Login(Login login)
        {
            _logger.Log(LogLevel.Trace, $"call #Login, login = {login}");

            User user = _userService.GetUserByUsername(login.Name);

            if (user == null || user.Password != login.Password)
            {
                throw new Exception($"Логін або пароль не вірні");
            }

            return Authorize(user);
        }

        public User Register(User user)
        {
            throw new NotImplementedException();
        }
    }
}
