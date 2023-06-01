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
        Topic GetTopic(int id);
        void Hide(Textual textual);
        void UnHide(Textual textual);
        void Like(Textual textual, User user);
        void Dislike(Textual textual, User user);
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
