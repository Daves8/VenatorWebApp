using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;
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

        [HttpPost("create-news")]
        [Authorize(Policy = AuthPolicy.MODERATOR_REQUIRE)]
        public void CreateNews(News news) => _contentService.CreateNews(news);

        [HttpGet("news/{id}")]
        public News GetNews(int id) => _contentService.GetNews(id);

        [HttpGet("all-news")]
        [Authorize(Policy = AuthPolicy.MODERATOR_REQUIRE)]
        public IEnumerable<News> GetAllNews() => _contentService.GetAllNews();

        [HttpGet("not-hidden-news")]
        public IEnumerable<News> GetAllNotHiddenNews() => _contentService.GetAllNews(false);

        [HttpPost("create-comment-to-news")]
        [Authorize]
        public void CreateCommentToNews(Comment comment) => _contentService.CreateComment(comment);

        [HttpGet("all-comments-to-news/{id}")]
        [Authorize(Policy = AuthPolicy.MODERATOR_REQUIRE)]
        public IEnumerable<Comment> GetAllCommentsToNews(int id) => _contentService.GetAllComments(new News(id));

        [HttpGet("not-hidden-comments-to-news/{id}")]
        public IEnumerable<Comment> GetAllNotHiddenCommentsToNews(int id) => _contentService.GetAllComments(new News(id), false);

        [HttpPost("like-news")]
        [Authorize]
        public void LikeNews(News news) => _contentService.Like(news);

        [HttpPost("dislike-news")]
        [Authorize]
        public void DislikeNews(News news) => _contentService.Dislike(news);

        [HttpPost("like-comment")]
        [Authorize]
        public void LikeComment(Comment news) => _contentService.Like(news);

        [HttpPost("dislike-comment")]
        [Authorize]
        public void DislikeComment(Comment news) => _contentService.Dislike(news);

    }
}
