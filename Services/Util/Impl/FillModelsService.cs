using VenatorWebApp.DAL;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Abstracts.Base;

namespace VenatorWebApp.Services.Util.Impl
{
    public class FillModelsService : IFillModelsService
    {
        protected IUserDao _userDao;
        protected IItemDao _itemDao;
        protected IMessageDao _messageDao;
        protected IContentDao _contentDao;

        public FillModelsService(IUserDao userDao, IItemDao itemDao, IMessageDao messageDao, IContentDao contentDao)
        {
            _userDao = userDao;
            _itemDao = itemDao;
            _messageDao = messageDao;
            _contentDao = contentDao;
        }

        public void Fill(BaseEntity entity)
        {
            switch (entity)
            {
                case User:
                    FillUser((User)entity);
                    break;
                case Item:
                    FillItem((Item)entity);
                    break;
                case Message:
                    FillMessage((Message)entity);
                    break;
                case News:
                    FillNews((News)entity);
                    break;
                case Topic:
                    FillTopic((Topic)entity);
                    break;
                case Comment:
                    FillComment((Comment)entity);
                    break;
            }
        }

        protected void FillUser(User user) { }

        protected void FillItem(Item item) { }

        protected void FillMessage(Message message)
        {
            message.Owner = _userDao.QueryUser(message.OwnerId);
            message.ToUser = _userDao.QueryUser(message.ToUserId);
        }

        protected void FillNews(News news)
        {
            news.Owner = _userDao.QueryUser(news.OwnerId);
        }

        protected void FillTopic(Topic topic)
        {
            topic.Owner = _userDao.QueryUser(topic.OwnerId);
        }

        protected void FillComment(Comment comment)
        {
            comment.Parent = comment.ParentType switch
            {
                Models.Common.TextualType.News => _contentDao.QueryNews(comment.ParentId),
                Models.Common.TextualType.Topic => _contentDao.QueryTopic(comment.ParentId),
                _ => throw new ArgumentException(),
            };
            comment.Owner = _userDao.QueryUser(comment.OwnerId);
        }

        protected void FillStatistics(Statistics statistics)
        {
            statistics.Owner = _userDao.QueryUser(statistics.OwnerId);
        }
    }
}