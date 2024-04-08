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
        public IActionResult Buy(UserDeliveryInfo userDeliveryInfo)
        {
            if (ModelState.IsValid)
            {
                var existingCart = cartsStorage.TryGetByUserId(Constants.UserId);
                var order = new Order
                {
                    UserDeliveryInfo = userDeliveryInfo,
                    Items = existingCart.Items
                };
                ordersStorage.Add(order);
                cartsStorage.Clear(Constants.UserId);
                return View();
            }
            return View();
        }
    }
}
