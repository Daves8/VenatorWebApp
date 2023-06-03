using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Models;
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

        [HttpPost("comm")]
        public void Comm(Comment comment)
        {
            _contentService.CreateComment(comment);
        }

        [HttpPost("getco")]
        public IEnumerable<Comment> GetComm(Models.Abstracts.Textual parent)
        {
            return _contentService.GetAllComments(parent);
        }

    }
}
