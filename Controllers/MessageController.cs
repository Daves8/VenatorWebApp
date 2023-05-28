using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Services;

namespace VenatorWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("test")]
        public string Test() => "Ok";
    }
}
