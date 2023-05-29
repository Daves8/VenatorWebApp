using VenatorWebApp.Models;

namespace VenatorWebApp.DAL
{
    public interface IMessageDao
    {
        Message QueryMessage(int id);
        IEnumerable<Message> QueryMessagesBetweenUsers(User user1, User user2);
        IEnumerable<Message> QueryMessagesWithUser(User user);
        Message QueryLastMessageWithUser(User user);
        void CreateMessage(Message message);
        void UpdateMessage(Message message);
        void DeleteMessage(Message message);
    }
}
