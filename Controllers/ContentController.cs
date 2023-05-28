using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Services;

namespace VenatorWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("test")]
        public string Test() => "Ok";
    }
}
