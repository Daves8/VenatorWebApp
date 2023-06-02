using Dapper;
using VenatorWebApp.DAL.Base;
using VenatorWebApp.DAL.Mapper;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;

namespace VenatorWebApp.DAL.Impl
{
    public class ItemDao : BaseDao, IItemDao
    {
        public ItemDao(IConfiguration config) : base(config)
            => SqlMapper.SetTypeMap(typeof(Item), CustomMapper.GetItemMapper());

        public void AddItemToUser(Item item, User user, ItemIn itemIn)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                USER_ID = user.Id,
                ITEM_ID = item.Id,
                IN_CART = itemIn
            };
            connection.Execute("DBO.ADD_ITEM_IN_USER", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void CreateItem(Item item)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                NAME = item.Name,
                DESCRIPTION = item.Description,
                CATEGORY = item.Category,
                PRICE = item.Price,
                IS_HIDDEN = item.IsHidden,
                IMAGE_URL = item.ImageUrl
            };
            connection.Execute("DBO.CREATE_ITEM", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteAllItemsInUser(User user, ItemIn itemIn)
        {
            using var connection = GetConnection();
            connection.Execute("DBO.DELETE_ITEMS_IN_USER", new { USER_ID = user.Id, IN_CART = itemIn }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteItem(Item item)
        {
            using var connection = GetConnection();
            connection.Execute("DBO.DELETE_ITEM", new { ID = item.Id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void DeleteItemInUser(Item item, User user, ItemIn itemIn)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                USER_ID = user.Id,
                ITEM_ID = item.Id,
                IN_CART = itemIn
            };
            connection.Execute("DBO.DELETE_ITEM_IN_USER", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void ModifyItemInUser(Item item, User user, ItemIn itemIn)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                USER_ID = user.Id,
                ITEM_ID = item.Id,
                IN_CART = itemIn
            };
            connection.Execute("DBO.UPDATE_ITEM_IN_USER", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<Item> QueryAllItems()
        {
            using var connection = GetConnection();
            return connection.Query<Item>("DBO.QUERY_ITEMS", commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<Item> QueryAllItems(bool isHidden)
        {
            using var connection = GetConnection();
            return connection.Query<Item>("DBO.QUERY_ITEMS_BY_IS_HIDDEN", new { IS_HIDDEN = isHidden }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public IEnumerable<Item> QueryAllItemsInUser(User user, ItemIn itemIn)
        {
            using var connection = GetConnection();
            return connection.Query<Item>("DBO.QUERY_ITEMS_IN_USER", new { USER_ID = user.Id, IN_CART = itemIn }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public Item QueryItem(int id)
        {
            using var connection = GetConnection();
            return connection.QueryFirstOrDefault<Item>("DBO.QUERY_ITEM_BY_ID", new { ID = id }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public void UpdateItem(Item item)
        {
            using var connection = GetConnection();
            var parameters = new
            {
                ID = item.Id,
                NAME = item.Name,
                DESCRIPTION = item.Description,
                CATEGORY = item.Category,
                PRICE = item.Price,
                IS_HIDDEN = item.IsHidden,
                IMAGE_URL = item.ImageUrl
            };
            connection.Execute("DBO.UPDATE_ITEM", parameters, commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
