using VenatorWebApp.DAL;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Services.Base;
using VenatorWebApp.Services.Exceptions;
using VenatorWebApp.Services.Util;

namespace VenatorWebApp.Services.Impl
{
    public class ItemService : BaseService, IItemService
    {
        private readonly IItemDao _itemDao;
        private readonly IUserService _userService;
        private readonly ILogger<ItemService> _logger;

        public ItemService(IItemDao itemDao, IUserService userService, IFillModelsService fillModelsService, ILogger<ItemService> logger) : base(fillModelsService)
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

        public IEnumerable<Item> GetAllItems() => _itemDao.QueryAllItems().ToList().Select(o => { return Fill(o); });

        public IEnumerable<Item> GetAllItemsInCart(User user) => _itemDao.QueryAllItemsInUser(user, ItemIn.Cart).ToList().Select(o => { return Fill(o); });

        public IEnumerable<Item> GetAllNotHiddenItems() => _itemDao.QueryAllItems(false).ToList().Select(o => { return Fill(o); });

        public IEnumerable<Item> GetAllPurchasedItems(User user) => _itemDao.QueryAllItemsInUser(user, ItemIn.Inventory).ToList().Select(o => { return Fill(o); });

        public Item GetItem(int id) => Fill(_itemDao.QueryItem(id));

        public IEnumerable<Item> GetRecomendedItems(User user)
        {
            //TODO: implement
            throw new NotImplementedException();
        }

        public void RemoveAllItemsFromCart(User user) => _itemDao.DeleteAllItemsInUser(user, ItemIn.Cart);

        public void RemoveItemFromCart(Item item, User user) => _itemDao.DeleteItemInUser(item, user, ItemIn.Cart);

        public void UpdateItem(Item item) => _itemDao.UpdateItem(item);
    }
}
