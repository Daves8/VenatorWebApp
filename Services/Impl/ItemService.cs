using VenatorWebApp.DAL;
using VenatorWebApp.Models;

namespace VenatorWebApp.Services.Impl
{
    public class ItemService : IItemService
    {
        private readonly IItemDao _itemDao;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IItemDao itemDao, ILogger<ItemService> logger)
        {
            _itemDao = itemDao;
            _logger = logger;
        }

        public void AddItemToCart(Item item, User user)
        {
            throw new NotImplementedException();
        }

        public void BuyItem(Item item, User user)
        {
            throw new NotImplementedException();
        }

        public void BuyItems(IEnumerable<Item> items, User user)
        {
            throw new NotImplementedException();
        }

        public void BuyItemsInCart(User user)
        {
            throw new NotImplementedException();
        }

        public void CreateItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(Item item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAllItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAllItemsInCart(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAllNotHiddenItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetAllPurchasedItems(User user)
        {
            throw new NotImplementedException();
        }

        public Item GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetRecomendedItems(User user)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllItemsFromCart(User user)
        {
            throw new NotImplementedException();
        }

        public void RemoveItemFromCart(Item item, User user)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
