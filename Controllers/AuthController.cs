using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Models;
using VenatorWebApp.Services;

namespace VenatorWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("test")]
        public string Test() => "Ok";

        [HttpPost("login")]
        public User Login(Login login)
        {
            var result = _authService.Login(login);
            Response.Headers.Add("Authorization", "Bearer " + result.token);
            return result.user;
        }

        [HttpPost("registration")]
        public User Registration(User user)
        {
            var result = _authService.Register(user);
            Response.Headers.Add("Authorization", "Bearer " + result.token);
            return result.user;
        }

        [HttpGet("current-user")]
        public User GetCurrentUser() => _authService.GetCurrentUser();
    }
}