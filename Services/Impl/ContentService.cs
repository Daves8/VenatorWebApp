using VenatorWebApp.DAL;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.Services.Impl
{
    public class ContentService : IContentService
    {
        private readonly IContentDao _contentDao;
        private readonly ILogger<ContentService> _logger;

        public ContentService(IContentDao contentDao, ILogger<ContentService> logger)
        {
            _contentDao = contentDao;
            _logger = logger;
        }

        public void CreateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void CreateNews(News news)
        {
            throw new NotImplementedException();
        }

        public void CreateReaction(Textual textual, ReactionType type)
        {
            throw new NotImplementedException();
        }

        public void CreateTopic(Topic topic)
        {
            throw new NotImplementedException();
        }

        public void DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void DeleteNews(News news)
        {
            throw new NotImplementedException();
        }

        public void DeleteTopic(Topic topic)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAllComments(Textual parent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> GetAllNews()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> GetAllTopics()
        {
            throw new NotImplementedException();
        }

        public News GetNews(int id)
        {
            throw new NotImplementedException();
        }

        public News GetTopic(int id)
        {
            throw new NotImplementedException();
        }

        public void Hide(Textual textual)
        {
            throw new NotImplementedException();
        }

        public void UpdateComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public void UpdateNews(News news)
        {
            throw new NotImplementedException();
        }

        public void UpdateTopic(Topic topic)
        {
            throw new NotImplementedException();
        }
    }
}
