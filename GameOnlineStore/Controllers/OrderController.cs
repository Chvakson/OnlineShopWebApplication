using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;

namespace GameOnlineStore.Controllers
{
    public class OrderController : Controller
    {
        public IOrdersStorage ordersStorage { get; set; }
        private readonly ICartsStorage cartsStorage;
        public OrderController(IOrdersStorage ordersStorage, ICartsStorage cartsStorage)
        {
            this.ordersStorage = ordersStorage;
            this.cartsStorage = cartsStorage;
        }
        public IActionResult Index(Cart cart)
        {
            var orders = ordersStorage.TryGetByUserId(Constants.UserId);
            return View(orders);
        }

        public IActionResult Add(Cart cart)
        {
            ordersStorage.Add(cart, Constants.UserId);
            return RedirectToAction("Clear", "Cart");
        }
    }
}
