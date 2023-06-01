using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Models;

namespace VenatorWebApp.DAL
{
    public interface IContentDao
    {
        IEnumerable<News> QueryAllNews();
        IEnumerable<Topic> QueryAllTopics();
        IEnumerable<Comment> QueryAllComments(Textual parent);
        void CreateReaction(Textual textual, User user, ReactionType type);
        void ModifyReaction(Textual textual, User user, ReactionType type);
        void DeleteReaction(Textual textual, User user);
        bool CheckReaction(Textual textual, User user, ReactionType type);
        News QueryNews(int id);
        void CreateNews(News news);
        void UpdateNews(News news);
        void DeleteNews(News news);
        Topic QueryTopic(int id);
        void CreateTopic(Topic topic);
        void UpdateTopic(Topic topic);
        void DeleteTopic(Topic topic);
        Comment QueryComment(int id);
        void CreateComment(Comment comment);
        void UpdateComment(Comment comment);
        void DeleteComment(Comment comment);
    }
}
