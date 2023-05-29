using VenatorWebApp.DAL;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Services.Exceptions;

namespace VenatorWebApp.Services.Impl
{
    public class ItemService : IItemService
    {
        private readonly IItemDao _itemDao;
        private readonly IUserService _userService;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IItemDao itemDao, IUserService userService, ILogger<ItemService> logger)
        {
            _itemDao = itemDao;
            _userService = userService;
            _logger = logger;
        }

        public void AddItemToCart(Item item, User user) => _itemDao.AddItemToUser(item, user, ItemIn.Cart);

        public void BuyItem(Item item, User user)
        {
            throw new NotImplementedException();
        }

        public void BuyItemsInCart(User user)
        {
            User queriedUser = _userService.GetUser(user.Id);
            IEnumerable<Item> itemsInUserCart = GetAllItemsInCart(user);
            double itemsInUserPriceSum = itemsInUserCart?.Sum(item => item.Price) ?? 0;

            if (queriedUser.GoldAmount >= itemsInUserPriceSum)
            {
                queriedUser.GoldAmount -= itemsInUserPriceSum;
                _userService.UpdateUser(queriedUser);

                foreach (var item in itemsInUserCart)
                {
                    _itemDao.ModifyItemInUser(item, user, ItemIn.Inventory);
                }
            }
            throw new HttpResponseException("Недостатньо коштів для покупки");
        }

        public void CreateItem(Item item) => _itemDao.CreateItem(item);

        public void DeleteItem(Item item) => _itemDao.DeleteItem(item);

        public IEnumerable<Item> GetAllItems() => _itemDao.QueryAllItems();

        public IEnumerable<Item> GetAllItemsInCart(User user) => _itemDao.QueryAllItemsInUser(user, ItemIn.Cart);

        public IEnumerable<Item> GetAllNotHiddenItems() => _itemDao.QueryAllItems(false);

        public IEnumerable<Item> GetAllPurchasedItems(User user) => _itemDao.QueryAllItemsInUser(user, ItemIn.Inventory);

        public Item GetItem(int id) => _itemDao.QueryItem(id);

        public IEnumerable<Item> GetRecomendedItems(User user)
        {
            throw new NotImplementedException();
        }

        public void RemoveAllItemsFromCart(User user) => _itemDao.DeleteAllItemsInUser(user, ItemIn.Cart);

        public void RemoveItemFromCart(Item item, User user) => _itemDao.DeleteItemInUser(item, user, ItemIn.Cart);

        public void UpdateItem(Item item) => _itemDao.UpdateItem(item);
    }
}
