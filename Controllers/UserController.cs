using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Models;
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

        [HttpGet("{id}")]
        public User GetUser(int id) => _userService.GetUser(id);

        [HttpGet]
        //[Authorize(Policy = AuthPolicy.ADMINISTRATOR_REQUIRE)]
        public IEnumerable<User> GetUsers() => _userService.GetAllUsers();
    }
}
