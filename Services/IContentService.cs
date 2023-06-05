using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models;

namespace VenatorWebApp.Services
{
    public interface IContentService
    {
        IEnumerable<News> GetAllNews();
        IEnumerable<News> GetAllNews(bool isHidden);
        IEnumerable<Topic> GetAllTopics();
        IEnumerable<Topic> GetAllTopics(bool isHidden);
        IEnumerable<Comment> GetAllComments(Textual parent);
        IEnumerable<Comment> GetAllComments(Textual parent, bool isHidden);
        News GetNews(int id);
        Topic GetTopic(int id);
        Comment GetComment(int id);
        void Hide(Textual textual);
        void UnHide(Textual textual);
        void Like(Textual textual);
        void Dislike(Textual textual);
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
