using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.DAL.Impl
{
    public class ItemDao : IItemDao
    {
        public void AddItemToUser(Item item, User user, ItemIn itemIn)
        {
            throw new NotImplementedException();
        }

        public void CreateItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void DeleteAllItemsInUser(User user, ItemIn itemIn)
        {
            throw new NotImplementedException();
        }

        public void DeleteItem(Item item)
        {
            throw new NotImplementedException();
        }

        public void DeleteItemInUser(Item item, User user, ItemIn itemIn)
        {
            throw new NotImplementedException();
        }

        public void ModifyItemInUser(Item item, User user, ItemIn itemIn)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> QueryAllItems()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> QueryAllItems(bool isHidden)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> QueryAllItemsInUser(User user, ItemIn itemIn)
        {
            throw new NotImplementedException();
        }

        public Item QueryItem(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
