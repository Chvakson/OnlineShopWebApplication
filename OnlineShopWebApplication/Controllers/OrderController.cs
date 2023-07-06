using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartsStorage cartsStorage;
        private readonly IOrdersStorage ordersStorage;
        public OrderController(ICartsStorage cartsStorage, IOrdersStorage ordersStorage)
        {
            this.cartsStorage = cartsStorage;
            this.ordersStorage = ordersStorage;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void Buy(UserData userData)
        {
            var existingCart = cartsStorage.TryGetByUserId(Constants.UserId);
            ordersStorage.Add(existingCart);
            ordersStorage.Add(userData);
            cartsStorage.Clear(Constants.UserId);
            //return View();
        }
    }
}
