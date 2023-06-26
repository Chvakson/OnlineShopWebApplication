using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Buy()
        {
            var existingCart = cartsStorage.TryGetByUserId(Constants.UserId);
            ordersStorage.Add(existingCart);
            cartsStorage.Clear(Constants.UserId);
            return RedirectToAction("Index", "Cart");
        }
    }
}
