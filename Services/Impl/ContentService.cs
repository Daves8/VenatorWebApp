using VenatorWebApp.DAL;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Services.Base;
using VenatorWebApp.Services.Exceptions;
using VenatorWebApp.Services.Util;

namespace VenatorWebApp.Services.Impl
{
    public class ContentService : BaseService, IContentService
    {
        private readonly IContentDao _contentDao;
        private readonly ILogger<ContentService> _logger;

        public ContentService(IContentDao contentDao, IFillModelsService fillModelsService, ILogger<ContentService> logger) : base(fillModelsService)
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

        public void Dislike(Textual textual, User user)
        {
            bool isLikeExists = _contentDao.CheckReaction(textual, user, ReactionType.Like);
            bool isDislikeExists = _contentDao.CheckReaction(textual, user, ReactionType.Dislike);

            if (isLikeExists)
            {
                switch (textual)
                {
                    case Comment:
                        var queriedComment = _contentDao.QueryComment(textual.Id);
                        queriedComment.LikesCount -= 1;
                        queriedComment.DislikesCount += 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = _contentDao.QueryNews(textual.Id);
                        queriedNews.LikesCount -= 1;
                        queriedNews.DislikesCount += 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = _contentDao.QueryTopic(textual.Id);
                        queriedTopic.LikesCount -= 1;
                        queriedTopic.DislikesCount += 1;
                        _contentDao.UpdateTopic(queriedTopic);
                        break;
                    default:
                        throw new ArgumentException();
                }
                _contentDao.ModifyReaction(textual, user, ReactionType.Dislike);
            }
            else if (!isDislikeExists)
            {
                switch (textual)
                {
                    case Comment:
                        var queriedComment = _contentDao.QueryComment(textual.Id);
                        queriedComment.DislikesCount += 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = _contentDao.QueryNews(textual.Id);
                        queriedNews.DislikesCount += 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = _contentDao.QueryTopic(textual.Id);
                        queriedTopic.DislikesCount += 1;
                        _contentDao.UpdateTopic(queriedTopic);
                        break;
                    default:
                        throw new ArgumentException();
                }
                _contentDao.CreateReaction(textual, user, ReactionType.Dislike);
            }
            else
            {
                switch (textual)
                {
                    case Comment:
                        var queriedComment = _contentDao.QueryComment(textual.Id);
                        queriedComment.DislikesCount -= 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = _contentDao.QueryNews(textual.Id);
                        queriedNews.DislikesCount -= 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = _contentDao.QueryTopic(textual.Id);
                        queriedTopic.DislikesCount -= 1;
                        _contentDao.UpdateTopic(queriedTopic);
                        break;
                    default:
                        throw new ArgumentException();
                }
                _contentDao.DeleteReaction(textual, user);
            }

        }

        public IEnumerable<Comment> GetAllComments(Textual parent) => _contentDao.QueryAllComments(parent).ToList().Select(o => { return Fill(o); });

        public IEnumerable<News> GetAllNews() => _contentDao.QueryAllNews().ToList().Select(o => { return Fill(o); });

        public IEnumerable<Topic> GetAllTopics() => _contentDao.QueryAllTopics().ToList().Select(o => { return Fill(o); });

        public News GetNews(int id) => Fill(_contentDao.QueryNews(id));

        public Topic GetTopic(int id) => Fill(_contentDao.QueryTopic(id));

        public void Hide(Textual textual) => ChangeVisibility(textual, true);

        public void Like(Textual textual, User user)
        {
            bool isLikeExists = _contentDao.CheckReaction(textual, user, ReactionType.Like);
            bool isDislikeExists = _contentDao.CheckReaction(textual, user, ReactionType.Dislike);

            if (isDislikeExists)
            {
                switch (textual)
                {
                    case Comment:
                        var queriedComment = _contentDao.QueryComment(textual.Id);
                        queriedComment.LikesCount += 1;
                        queriedComment.DislikesCount -= 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = _contentDao.QueryNews(textual.Id);
                        queriedNews.LikesCount += 1;
                        queriedNews.DislikesCount -= 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = _contentDao.QueryTopic(textual.Id);
                        queriedTopic.LikesCount += 1;
                        queriedTopic.DislikesCount -= 1;
                        _contentDao.UpdateTopic(queriedTopic);
                        break;
                    default:
                        throw new ArgumentException();
                }
                _contentDao.ModifyReaction(textual, user, ReactionType.Like);
            }
            else if (!isLikeExists)
            {
                switch (textual)
                {
                    case Comment:
                        var queriedComment = _contentDao.QueryComment(textual.Id);
                        queriedComment.LikesCount += 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = _contentDao.QueryNews(textual.Id);
                        queriedNews.LikesCount += 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = _contentDao.QueryTopic(textual.Id);
                        queriedTopic.LikesCount += 1;
                        _contentDao.UpdateTopic(queriedTopic);
                        break;
                    default:
                        throw new ArgumentException();
                }
                _contentDao.CreateReaction(textual, user, ReactionType.Like);
            }
            else
            {
                switch (textual)
                {
                    case Comment:
                        var queriedComment = _contentDao.QueryComment(textual.Id);
                        queriedComment.LikesCount -= 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = _contentDao.QueryNews(textual.Id);
                        queriedNews.LikesCount -= 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = _contentDao.QueryTopic(textual.Id);
                        queriedTopic.LikesCount -= 1;
                        _contentDao.UpdateTopic(queriedTopic);
                        break;
                    default:
                        throw new ArgumentException();
                }
                _contentDao.DeleteReaction(textual, user);
            }
        }

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
