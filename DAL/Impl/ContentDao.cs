using Dapper;
using VenatorWebApp.DAL.Base;
using VenatorWebApp.DAL.Mapper;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Abstracts;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.DAL.Impl
{
    public class ContentDao : BaseDao, IContentDao
    {
        public ContentDao(IConfiguration config) : base(config)
        {
            SetTypeMap(typeof(News));
            SetTypeMap(typeof(Topic));
            SetTypeMap(typeof(Comment));
        }

        public bool CheckReaction(Textual textual, User user, ReactionType type)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                REACTION_TYPE = type,
                TEXTUAL_ID = textual.Id,
                TEXTUAL_TYPE = TextualTypeConvertion.GetTextualType(textual),
                USER_ID = user.Id
            };
            return connection.QueryFirstOrDefault<bool>("DBO.CHECK_REACTION_EXISTENCE", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void CreateComment(Comment comment)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                TEXT = comment.Text,
                PARENT_TYPE_ID = comment.ParentType,
                //TODO: add support
                //PARENT_TYPE_ID = TextualTypeConvertion.GetTextualType(comment.Parent),
                PARENT_ID = comment.Parent.Id,
                USER_ID = comment.Owner.Id,
                LIKES_COUNT = 0,
                DISLIKES_COUNT = 0,
                IS_HIDDEN = 0
            };
            connection.Execute("DBO.CREATE_COMMENT", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void CreateNews(News news)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                TITLE = news.Name,
                TEXT = news.Text,
                METRICS = news.Metrics,
                USER_ID = news.Owner.Id,
                LIKES_COUNT = 0,
                DISLIKES_COUNT = 0,
                IS_HIDDEN = 0
            };
            connection.Execute("DBO.CREATE_NEWS", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void CreateReaction(Textual textual, User user, ReactionType type)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                REACTION_TYPE = type,
                TEXTUAL_ID = textual.Id,
                TEXTUAL_TYPE = TextualTypeConvertion.GetTextualType(textual),
                USER_ID = user.Id,
            };
            connection.Execute("DBO.CREATE_REACTION", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void CreateTopic(Topic topic)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                TITLE = topic.Name,
                TEXT = topic.Text,
                METRICS = topic.Metrics,
                USER_ID = topic.Owner.Id,
                LIKES_COUNT = 0,
                DISLIKES_COUNT = 0,
                IS_HIDDEN = 0
            };
            connection.Execute("DBO.CREATE_TOPIC", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteComment(Comment comment)
        {
            using var connection = GetConnection();
            connection.Execute("DBO.DELETE_COMMENT", new { ID = comment.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteNews(News news)
        {
            using var connection = GetConnection();
            connection.Execute("DBO.DELETE_NEWS", new { ID = news.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteReaction(Textual textual, User user)
        {
            using var connection = GetConnection();
            connection.Execute("DBO.DELETE_REACTION",
                new { TEXTUAL_ID = textual.Id, TEXTUAL_TYPE = TextualTypeConvertion.GetTextualType(textual), USER_ID = user.Id },
                commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteTopic(Topic topic)
        {
            using var connection = GetConnection();
            connection.Execute("DBO.DELETE_TOPIC", new { ID = topic.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void ModifyReaction(Textual textual, User user, ReactionType type)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                REACTION_TYPE = type,
                TEXTUAL_ID = textual.Id,
                TEXTUAL_TYPE = TextualTypeConvertion.GetTextualType(textual),
                USER_ID = user.Id,
            };
            connection.Execute("DBO.UPDATE_REACTION", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<Comment> QueryAllComments(Textual parent)
        {
            using var connection = GetConnection();
            return connection.Query<Comment>("DBO.QUERY_COMMENTS", new { PARENT_ID = parent.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<News> QueryAllNews()
        {
            using var connection = GetConnection();
            return connection.Query<News>("DBO.QUERY_NEWS", commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<Topic> QueryAllTopics()
        {
            using var connection = GetConnection();
            return connection.Query<Topic>("DBO.QUERY_TOPICS", commandType: System.Data.CommandType.StoredProcedure);
        }

        public Comment QueryComment(int id)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<Comment>("DBO.QUERY_COMMENT_BY_ID", new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public News QueryNews(int id)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<News>("DBO.QUERY_NEWS_BY_ID", new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public Topic QueryTopic(int id)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<Topic>("DBO.QUERY_TOPIC_BY_ID", new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void UpdateComment(Comment comment)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                ID = comment.Id,
                TEXT = comment.Text,
                PARENT_TYPE_ID = comment.ParentType,
                //TODO: add support
                //PARENT_TYPE_ID = TextualTypeConvertion.GetTextualType(comment.Parent),
                PARENT_ID = comment.Parent.Id,
                USER_ID = comment.Owner.Id,
                LIKES_COUNT = comment.LikesCount,
                DISLIKES_COUNT = comment.DislikesCount,
                IS_HIDDEN = comment.IsHidden
            };
            connection.Execute("DBO.UPDATE_COMMENT", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void UpdateNews(News news)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                ID = news.Id,
                TITLE = news.Name,
                TEXT = news.Text,
                METRICS = news.Metrics,
                USER_ID = news.Owner.Id,
                LIKES_COUNT = news.LikesCount,
                DISLIKES_COUNT = news.DislikesCount,
                IS_HIDDEN = news.IsHidden
            };
            connection.Execute("DBO.UPDATE_NEWS", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void UpdateTopic(Topic topic)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                ID = topic.Id,
                TITLE = topic.Name,
                TEXT = topic.Text,
                METRICS = topic.Metrics,
                USER_ID = topic.Owner.Id,
                LIKES_COUNT = topic.LikesCount,
                DISLIKES_COUNT = topic.DislikesCount,
                IS_HIDDEN = topic.IsHidden
            };
            connection.Execute("DBO.UPDATE_TOPIC", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        protected void SetTypeMap(Type type) => SqlMapper.SetTypeMap(type, CustomMapper.GetMapperByType(type));
    }
}
