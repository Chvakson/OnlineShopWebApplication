using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartsStorage cartsStorage;
        public OrderController(ICartsStorage cartsStorage)
        {
            this.cartsStorage = cartsStorage;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Arrange()
        {
            cartsStorage.Clear(Constants.UserId);
            return RedirectToAction("Index", "Cart");
        }
    }
}
