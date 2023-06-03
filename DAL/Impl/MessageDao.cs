using Dapper;
using VenatorWebApp.DAL.Base;
using VenatorWebApp.DAL.Mapper;
using VenatorWebApp.Models;

namespace VenatorWebApp.DAL.Impl
{
    public class MessageDao : BaseDao, IMessageDao
    {
        public MessageDao(IConfiguration config) : base(config)
            => SqlMapper.SetTypeMap(typeof(Message), CustomMapper.GetMessageMapper());

        public void CreateMessage(Message message)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                TEXT = message.Text,
                FROM_USER_ID = message.Owner.Id,
                TO_USER_ID = message.ToUser.Id,
                PARENT_MESSAGE_ID = message.Parent?.Id,
                IS_HIDDEN = message.IsHidden
            };
            connection.Execute("DBO.CREATE_MESSAGE", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteMessage(Message message)
        {
            using var connection = GetConnection();
            connection.Execute("DBO.DELETE_MESSAGE", new { ID = message.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<Message> QueryMessagesBetweenUsers(User user1, User user2)
        {
            using var connection = GetConnection();
            return connection.Query<Message>("DBO.QUERY_MESSAGES_BETWEEN_USERS",
                new { USER_ONE_ID = user1.Id, USER_TWO_ID = user2.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public Message QueryLastMessageWithUsers(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        public Message QueryMessage(int id)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<Message>("DBO.QUERY_MESSAGE_BY_ID", new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<Message> QueryMessagesWithUser(User user)
        {
            using var connection = GetConnection();
            return connection.Query<Message>("DBO.QUERY_MESSAGES_WITH_USER", new { USER_ID = user.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void UpdateMessage(Message message)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                ID = message.Id,
                TEXT = message.Text,
                FROM_USER_ID = message.Owner.Id,
                TO_USER_ID = message.ToUser.Id,
                PARENT_MESSAGE_ID = message.Parent?.Id,
                IS_HIDDEN = message.IsHidden
            };
            connection.Execute("DBO.UPDATE_MESSAGE", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
