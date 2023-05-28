using Microsoft.AspNetCore.Mvc;
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
    }
}