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
        private readonly IAuthService _authService;
        private readonly ILogger<ContentService> _logger;

        public ContentService(IContentDao contentDao, IAuthService authService, IFillModelsService fillModelsService, ILogger<ContentService> logger) : base(fillModelsService)
        {
            _contentDao = contentDao;
            _authService = authService;
            _logger = logger;
            _authService = authService;
        }

        public void CreateComment(Comment comment)
        {
            if (!comment.IsValid())
            {
                throw new HttpResponseException("Коментар пустий або перевищує максимальну довжину");
            }
            comment.Owner = _authService.GetCurrentUser();
            _contentDao.CreateComment(comment);
        }

        public void CreateNews(News news)
        {
            if (!news.IsValid())
            {
                throw new HttpResponseException("Новина пуста або перевищує максимальну довжину");
            }
            news.Owner = _authService.GetCurrentUser();
            _contentDao.CreateNews(news);
        }

        public void CreateTopic(Topic topic)
        {
            if (!topic.IsValid())
            {
                throw new HttpResponseException("Запис пустий або перевищує максимальну довжину");
            }
            topic.Owner = _authService.GetCurrentUser();
            _contentDao.CreateTopic(topic);
        }

        public void DeleteComment(Comment comment) => _contentDao.DeleteComment(comment);

        public void DeleteNews(News news) => _contentDao.DeleteNews(news);

        public void DeleteTopic(Topic topic) => _contentDao.DeleteTopic(topic);

        public void Dislike(Textual textual)
        {
            var user = _authService.GetCurrentUser();

            bool isLikeExists = _contentDao.CheckReaction(textual, user, ReactionType.Like);
            bool isDislikeExists = _contentDao.CheckReaction(textual, user, ReactionType.Dislike);

            if (isLikeExists)
            {
                switch (textual)
                {
                    case Comment:
                        var queriedComment = GetComment(textual.Id);
                        queriedComment.LikesCount -= 1;
                        queriedComment.DislikesCount += 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = GetNews(textual.Id);
                        queriedNews.LikesCount -= 1;
                        queriedNews.DislikesCount += 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = GetTopic(textual.Id);
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
                        var queriedComment = GetComment(textual.Id);
                        queriedComment.DislikesCount += 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = GetNews(textual.Id);
                        queriedNews.DislikesCount += 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = GetTopic(textual.Id);
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
                        var queriedComment = GetComment(textual.Id);
                        queriedComment.DislikesCount -= 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = GetNews(textual.Id);
                        queriedNews.DislikesCount -= 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = GetTopic(textual.Id);
                        queriedTopic.DislikesCount -= 1;
                        _contentDao.UpdateTopic(queriedTopic);
                        break;
                    default:
                        throw new ArgumentException();
                }
                _contentDao.DeleteReaction(textual, user);
            }
        }

        public IEnumerable<Comment> GetAllComments(Textual parent)
        {
            var allComments = _contentDao.QueryAllComments(parent).ToList().Select(o => { return Fill(o); });
            return FillTextualByUser(allComments);
        }

        public IEnumerable<Comment> GetAllComments(Textual parent, bool isHidden)
        {
            var allComments = _contentDao.QueryAllComments(parent).Where(o => o.IsHidden == isHidden).ToList().Select(o => { return Fill(o); });
            return FillTextualByUser(allComments);
        }

        public IEnumerable<News> GetAllNews()
        {
            var allNews = _contentDao.QueryAllNews().ToList().Select(o => { return Fill(o); });
            return FillTextualByUser(allNews);
        }

        public IEnumerable<News> GetAllNews(bool isHidden)
        {
            var allNews = _contentDao.QueryAllNews().Where(o => o.IsHidden == isHidden).ToList().Select(o => { return Fill(o); });
            return FillTextualByUser(allNews);
        }

        public IEnumerable<Topic> GetAllTopics()
        {
            var allTopicss = _contentDao.QueryAllTopics().ToList().Select(o => { return Fill(o); });
            return FillTextualByUser(allTopicss);
        }

        public IEnumerable<Topic> GetAllTopics(bool isHidden)
        {
            var allTopics = _contentDao.QueryAllTopics().Where(o => o.IsHidden == isHidden).ToList().Select(o => { return Fill(o); });
            return FillTextualByUser(allTopics);
        }

        public Comment GetComment(int id)
        {
            var comment = _contentDao.QueryComment(id);

            if (comment != null && comment.IsHidden && _authService.GetUserRole() == Role.User)
            {
                throw new HttpResponseException("Не має доступу", 401);
            }

            return FillTextualByUser(Fill(comment));
        }

        public News GetNews(int id)
        {
            var news = _contentDao.QueryNews(id);

            if (news != null && news.IsHidden && _authService.GetUserRole() == Role.User)
            {
                throw new HttpResponseException("Не має доступу", 401);
            }

            return FillTextualByUser(Fill(news));
        }

        public Topic GetTopic(int id)
        {
            var topic = _contentDao.QueryTopic(id);

            if (topic != null && topic.IsHidden && _authService.GetUserRole() == Role.User)
            {
                throw new HttpResponseException("Не має доступу", 401);
            }

            return FillTextualByUser(Fill(topic));
        }

        public void Hide(Textual textual) => ChangeVisibility(textual, true);

        public void Like(Textual textual)
        {
            var user = _authService.GetCurrentUser();

            bool isLikeExists = _contentDao.CheckReaction(textual, user, ReactionType.Like);
            bool isDislikeExists = _contentDao.CheckReaction(textual, user, ReactionType.Dislike);

            if (isDislikeExists)
            {
                switch (textual)
                {
                    case Comment:
                        var queriedComment = GetComment(textual.Id);
                        queriedComment.LikesCount += 1;
                        queriedComment.DislikesCount -= 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = GetNews(textual.Id);
                        queriedNews.LikesCount += 1;
                        queriedNews.DislikesCount -= 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = GetTopic(textual.Id);
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
                        var queriedComment = GetComment(textual.Id);
                        queriedComment.LikesCount += 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = GetNews(textual.Id);
                        queriedNews.LikesCount += 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = GetTopic(textual.Id);
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
                        var queriedComment = GetComment(textual.Id);
                        queriedComment.LikesCount -= 1;
                        _contentDao.UpdateComment(queriedComment);
                        break;
                    case News:
                        var queriedNews = GetNews(textual.Id);
                        queriedNews.LikesCount -= 1;
                        _contentDao.UpdateNews(queriedNews);
                        break;
                    case Topic:
                        var queriedTopic = GetTopic(textual.Id);
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
                    var queriedComment = GetComment(textual.Id);
                    queriedComment.IsHidden = value;
                    _contentDao.UpdateComment(queriedComment);
                    break;
                case News news:
                    var queriedNews = GetNews(textual.Id);
                    queriedNews.IsHidden = value;
                    _contentDao.UpdateNews(queriedNews);
                    break;
                case Topic topic:
                    var queriedTopic = GetTopic(textual.Id);
                    queriedTopic.IsHidden = value;
                    _contentDao.UpdateTopic(queriedTopic);
                    break;
                default:
                    throw new ArgumentException();
            }
        }

        protected IEnumerable<T> FillTextualByUser<T>(IEnumerable<T> textuals) where T : Textual
        {
            var currentUser = _authService.GetCurrentUser();

            if (currentUser != null)
            {
                textuals.ToList().ForEach(t => t.CurrentLike = _contentDao.CheckReaction(t, currentUser, ReactionType.Like));
                textuals.ToList().ForEach(t => t.CurrentDislike = _contentDao.CheckReaction(t, currentUser, ReactionType.Dislike));
            }

            return textuals;
        }

        protected T FillTextualByUser<T>(T textual) where T : Textual
        {
            var currentUser = _authService.GetCurrentUser();

            if (currentUser != null)
            {
                textual.CurrentLike = _contentDao.CheckReaction(textual, currentUser, ReactionType.Like);
                textual.CurrentDislike = _contentDao.CheckReaction(textual, currentUser, ReactionType.Dislike);
            }

            return textual;
        }
    }
}
