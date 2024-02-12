using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;

namespace GameOnlineStore.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersStorage ordersStorage;
        private readonly ICartsStorage cartsStorage;
        public OrderController(IOrdersStorage ordersStorage, ICartsStorage cartsStorage)
        {
            this.ordersStorage = ordersStorage;
            this.cartsStorage = cartsStorage;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Buy()
        {
            var existingCart = cartsStorage.TryGetByUserId(Constants.UserId);
            ordersStorage.Add(existingCart);
            cartsStorage.Clear(Constants.UserId);
            return View("Buy");
        }
    }
}
