using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Services;

namespace VenatorWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("test")]
        public string Test() => "Ok";
    }
}
