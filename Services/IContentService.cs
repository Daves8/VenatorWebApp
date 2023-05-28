using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.Services
{
    public interface IContentService
    {
        IEnumerable<News> GetAllNews();
        IEnumerable<Topic> GetAllTopics();
        IEnumerable<Comment> GetAllComments(Textual parent);
        News GetNews(int id);
        News GetTopic(int id);
        void Hide(Textual textual);
        void CreateReaction(Textual textual, ReactionType type);
        void CreateNews(News news);
        void UpdateNews(News news);
        void DeleteNews(News news);
        void CreateTopic(Topic topic);
        void UpdateTopic(Topic topic);
        void DeleteTopic(Topic topic);
        void CreateComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
