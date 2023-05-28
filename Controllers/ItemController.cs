using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Services;

namespace VenatorWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("test")]
        public string Test() => "Ok";
    }
}
