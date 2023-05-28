using VenatorWebApp.Models;

namespace VenatorWebApp.Services
{
    public interface IMessageService
    {
        Message GetMessage(int id);
        IEnumerable<Message> GetAllMessagesBetweenUsers(User user1, User user2);
        IEnumerable<Message> GetLastUserMessages(User user);
        IEnumerable<User> GetUsersWithMessages(User user);
        void CreateMessage(Message message);
        void UpdateMessage(Message message);
        void DeleteMessage(Message message);
    }
}
