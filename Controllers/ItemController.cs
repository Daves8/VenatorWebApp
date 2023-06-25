using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VenatorWebApp.Models;
using VenatorWebApp.Models.Common;
using VenatorWebApp.Services;

namespace VenatorWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet("test")]
        public string Test() => "Ok";

        [HttpGet("{id}")]
        public Item GetNews(int id) => _itemService.GetItem(id);

        [HttpGet("all-items")]
        [Authorize(Policy = AuthPolicy.MODERATOR_REQUIRE)]
        public IEnumerable<Item> GetAllItems() => _itemService.GetAllItems();

        [HttpGet("not-hidden-items")]
        public IEnumerable<Item> GetAllNotHiddenItems() => _itemService.GetAllNotHiddenItems();

        [HttpPost("add-to-cart")]
        [Authorize]
        public void AddItemToCart(Item item) => _itemService.AddItemToCart(item);

        [HttpGet("remove-all-items-from-cart")]
        [Authorize]
        public void RemoveAllItemsFromCart() => _itemService.RemoveAllItemsFromCart();

        [HttpGet("remove-item-from-cart/{id}")]
        [Authorize]
        public void RemoveItemFromCart(int id) => _itemService.RemoveItemFromCart(new Item(id));

        [HttpGet("buy-items-in-cart")]
        [Authorize]
        public void BuyItemsInCart() => _itemService.BuyItemsInCart();

        [HttpGet("items-in-cart")]
        [Authorize]
        public IEnumerable<Item> GetAllItemsInCart() => _itemService.GetAllItemsInCart();

        [HttpGet("items-in-inventory")]
        [Authorize]
        public IEnumerable<Item> GetAllItemsInInventory() => _itemService.GetAllPurchasedItems();

        [HttpGet("recommended-items")]
        [Authorize]
        public IEnumerable<Item> GetRecommendedItems() => _itemService.GetRecomendedItems();

        [HttpGet("predict")]
        public IEnumerable<Item> GetRecomendedItems() => _itemService.GetRecomendedItems();
    }
}
