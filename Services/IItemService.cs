using VenatorWebApp.Models;

namespace VenatorWebApp.Services
{
    public interface IItemService
    {
        Item GetItem(int id);
        IEnumerable<Item> GetAllItems();
        IEnumerable<Item> GetAllNotHiddenItems();
        IEnumerable<Item> GetAllItemsInCart();
        IEnumerable<Item> GetAllPurchasedItems();
        IEnumerable<Item> GetRecomendedItems();
        void AddItemToCart(Item item);
        void RemoveItemFromCart(Item item);
        void RemoveAllItemsFromCart();
        void BuyItem(Item item, User user);
        void BuyItemsInCart();
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Item item);
    }
}
