using VenatorWebApp.Models;

namespace VenatorWebApp.DAL.Impl
{
    public class MessageDao : IMessageDao
    {
        public void CreateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> QueryMessagesBetweenUsers(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        public Message QueryLastMessageWithUser(User user)
        {
            throw new NotImplementedException();
        }

        public Message QueryMessage(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> QueryMessagesWithUser(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
