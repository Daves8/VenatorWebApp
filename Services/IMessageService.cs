using VenatorWebApp.Models;

namespace VenatorWebApp.Services
{
    public interface IMessageService
    {
        Message GetMessage(int id);
        IEnumerable<Message> GetMessagesBetweenUsers(User user1, User user2);
        IEnumerable<Message> GetLastUserMessages(User user);
        IEnumerable<User> GetUsersWithMessages(User user);
        Message GetLastMessageBetweenUsers(User user1, User user2);
        void CreateMessage(Message message);
        void UpdateMessage(Message message);
        void DeleteMessage(Message message);
    }
}
