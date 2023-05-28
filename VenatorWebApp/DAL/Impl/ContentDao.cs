using VenatorWebApp.Models;
using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.DAL.Impl
{
    public class ContentDao : IContentDao
    {
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

        public IEnumerable<Comment> QueryAllComments(Textual parent)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<News> QueryAllNews()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Topic> QueryAllTopics()
        {
            throw new NotImplementedException();
        }

        public News QueryComment(int id)
        {
            throw new NotImplementedException();
        }

        public News QueryNews(int id)
        {
            throw new NotImplementedException();
        }

        public News QueryTopic(int id)
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
