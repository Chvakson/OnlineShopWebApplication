using GameOnlineStore.Models;
using GameOnlineStore.Db.Repositories.Carts;
using GameOnlineStore.Repositories.Orders;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using GameOnlineStore.Helpers;

namespace GameOnlineStore.Models.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersStorage ordersStorage;
        private readonly ICartsDbRepository cartsStorage;
        public OrderController(IOrdersStorage ordersStorage, ICartsDbRepository cartsStorage)
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
                var existingCartViewModel = Mapping.ToCartViewModel(existingCart);

                var order = new Order
                {
                    UserDeliveryInfo = userDeliveryInfo,
                    Items = existingCartViewModel.Items
                };
                ordersStorage.Add(order);
                cartsStorage.Clear(Constants.UserId);
                return View();
            }
            return View();
        }
    }
}
