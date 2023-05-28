using VenatorWebApp.Models.Common;
using VenatorWebApp.Models;

namespace VenatorWebApp.DAL
{
    public interface IItemDao
    {
        Item QueryItem(int id);
        IEnumerable<Item> QueryAllItems();
        IEnumerable<Item> QueryAllItems(bool isHidden);
        IEnumerable<Item> QueryAllItemsByUser(User user, ItemIn itemIn);
        void AddItemToUser(Item item, User user, ItemIn itemIn);
        void ModifyItemInUser(Item item, User user, ItemIn itemIn);
        void DeleteItemInUser(Item item, User user);
        void DeleteAllItemsInUser(User user, ItemIn itemIn);
        void CreateItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(Item item);
    }
}
