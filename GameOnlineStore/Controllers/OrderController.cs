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

        [HttpPost]
        public IActionResult Buy(UserAddress userAddress, UserContacts userContacts, string? comment)
        {
            var existingCart = cartsStorage.TryGetByUserId(Constants.UserId);
            ordersStorage.Add(existingCart, userAddress, userContacts, comment);
            cartsStorage.Clear(Constants.UserId);
            return View();
        }
    }
}
