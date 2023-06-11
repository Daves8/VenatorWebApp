using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImgController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        protected const string DEFAULT_IMG = "/img/default.png";

        public ImgController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet("test")]
        public void Test() { }

        [HttpGet("item/{category}/{name}")]
        public IActionResult GetItemImage(ItemCategory category, string name)
        {
            var path = _environment.WebRootPath + "/img/item/" + ((int)category);
            var imagePath = Path.Combine(path, name);

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg");
            }
            else
            {
                var imageBytes = System.IO.File.ReadAllBytes(_environment.WebRootPath + DEFAULT_IMG);
                return File(imageBytes, "image/jpeg");
            }
        }

        [HttpGet("user/{username}")]
        public IActionResult GetUserImage(string username)
        {
            var path = _environment.WebRootPath + "/img/user";
            var imagePath = Path.Combine(path, username);

            if (System.IO.File.Exists(imagePath))
            {
                var imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return File(imageBytes, "image/jpeg");
            }
            else
            {
                var imageBytes = System.IO.File.ReadAllBytes(_environment.WebRootPath + DEFAULT_IMG);
                return File(imageBytes, "image/jpeg");
            }
        }

        [HttpPost("item/{category}/{name}")]
        public async Task<IActionResult> SetItemImage(ItemCategory category, string name, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var path = _environment.WebRootPath + "/img/item/" + ((int)category);
                string imagePath = Path.Combine(path, name);
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("user/{username}")]
        public async Task<IActionResult> SetUserImage(string username, IFormFile image)
        {
            if (image != null && image.Length > 0)
            {
                var path = _environment.WebRootPath + "/img/user";
                string imagePath = Path.Combine(path, username);
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}
