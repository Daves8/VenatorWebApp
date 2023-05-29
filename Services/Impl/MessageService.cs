using VenatorWebApp.DAL;
using VenatorWebApp.Models;
using VenatorWebApp.Services.Exceptions;

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
            if (!message.IsValid())
            {
                throw new HttpResponseException("Не введені усі необхідні дані", 409);
            }
            _messageDao.CreateMessage(message);
        }

        public void DeleteMessage(Message message) => _messageDao.DeleteMessage(message);

        public IEnumerable<Message> GetMessagesBetweenUsers(User user1, User user2) => _messageDao.QueryMessagesBetweenUsers(user1, user2);

        public IEnumerable<Message> GetLastUserMessages(User user)
        {
            IEnumerable<User> usersWithMessages = GetUsersWithMessages(user);
            return usersWithMessages.Select(u => GetLastMessageBetweenUsers(user, u));
        }

        public Message GetMessage(int id) => _messageDao.QueryMessage(id);

        public IEnumerable<User> GetUsersWithMessages(User user)
        {
            IEnumerable<Message> messagesWithUser = _messageDao.QueryMessagesWithUser(user);
            IEnumerable<User> owners = messagesWithUser.Select(m => m.Owner).Distinct();
            IEnumerable<User> toUsers = messagesWithUser.Select(m => m.ToUser).Distinct();
            IList<User> result = owners.Union(toUsers).ToList();
            result.Remove(user);
            return result;
        }

        public void UpdateMessage(Message message)
        {
            if (!message.IsValid())
            {
                throw new HttpResponseException("Не введені усі необхідні дані", 409);
            }
            _messageDao.UpdateMessage(message);
        }

        public Message GetLastMessageBetweenUsers(User user1, User user2)
        {
            return GetMessagesBetweenUsers(user1, user2)?.OrderByDescending(m => m.CreationDate).FirstOrDefault() ?? null;
        }
    }
}
