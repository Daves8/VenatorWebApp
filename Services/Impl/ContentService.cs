using VenatorWebApp.DAL;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Services.Exceptions;

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
            if (!comment.IsValid())
            {
                throw new HttpResponseException("Коментар пустий або перевищує максимальну довжину");
            }
            _contentDao.CreateComment(comment);
        }

        public void CreateNews(News news)
        {
            if (!news.IsValid())
            {
                throw new HttpResponseException("Новина пуста або перевищує максимальну довжину");
            }
            _contentDao.CreateNews(news);
        }

        public void CreateReaction(Textual textual, ReactionType type) => _contentDao.CreateReaction(textual, type);

        public void CreateTopic(Topic topic)
        {
            if (!topic.IsValid())
            {
                throw new HttpResponseException("Запис пустий або перевищує максимальну довжину");
            }
            _contentDao.CreateTopic(topic);
        }

        public void DeleteComment(Comment comment) => _contentDao.DeleteComment(comment);

        public void DeleteNews(News news) => _contentDao.DeleteNews(news);

        public void DeleteTopic(Topic topic) => _contentDao.DeleteTopic(topic);

        public IEnumerable<Comment> GetAllComments(Textual parent) => _contentDao.QueryAllComments(parent);

        public IEnumerable<News> GetAllNews() => _contentDao.QueryAllNews();

        public IEnumerable<Topic> GetAllTopics() => _contentDao.QueryAllTopics();

        public News GetNews(int id) => _contentDao.QueryNews(id);

        public Topic GetTopic(int id) => _contentDao.QueryTopic(id);

        public void Hide(Textual textual) => ChangeVisibility(textual, true);

        public void UnHide(Textual textual) => ChangeVisibility(textual, false);

        public void UpdateComment(Comment comment) => _contentDao.UpdateComment(comment);

        public void UpdateNews(News news) => _contentDao.UpdateNews(news);

        public void UpdateTopic(Topic topic) => _contentDao.UpdateTopic(topic);

        protected void ChangeVisibility(Textual textual, bool value)
        {
            switch (textual)
            {
                case Comment comment:
                    var queriedComment = _contentDao.QueryComment(textual.Id);
                    queriedComment.IsHidden = value;
                    _contentDao.UpdateComment(queriedComment);
                    break;
                case News news:
                    var queriedNews = _contentDao.QueryNews(textual.Id);
                    queriedNews.IsHidden = value;
                    _contentDao.UpdateNews(queriedNews);
                    break;
                case Topic topic:
                    var queriedTopic = _contentDao.QueryTopic(textual.Id);
                    queriedTopic.IsHidden = value;
                    _contentDao.UpdateTopic(queriedTopic);
                    break;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
