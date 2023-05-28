using VenatorWebApp.DAL;
using VenatorWebApp.Models;

namespace VenatorWebApp.Services.Impl
{
    public class MessageService : IMessageService
    {
        private readonly IMessageDao _messageDao;
        private readonly ILogger<MessageService> _logger;

        public MessageService(IMessageDao messageDao, ILogger<MessageService> logger)
        {
            _messageDao = messageDao;
            _logger = logger;
        }

        public void CreateMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public void DeleteMessage(Message message)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetAllMessagesBetweenUsers(User user1, User user2)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Message> GetLastUserMessages(User user)
        {
            throw new NotImplementedException();
        }

        public Message GetMessage(int id)
        {
            throw new NotImplementedException();
        }

        public Message GetMessageById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetUsersWithMessages(User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
