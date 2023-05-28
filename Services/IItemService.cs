using VenatorWebApp.Models;

namespace VenatorWebApp.Services
{
    public interface IItemService
    {
        Item GetItem(int id);
        IEnumerable<Item> GetAllItems();
        IEnumerable<Item> GetAllNotHiddenItems();
        IEnumerable<Item> GetAllItemsInCart(User user);
        IEnumerable<Item> GetAllPurchasedItems(User user);
        IEnumerable<Item> GetRecomendedItems(User user);
        void AddItemToCart(Item item, User user);
        void RemoveItemFromCart(Item item, User user);
        void RemoveAllItemsFromCart(User user);
        void BuyItem(Item item, User user);
        void BuyItemsInCart(User user);
        void BuyItems(IEnumerable<Item> items, User user);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Item item);
    }
}
