using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Controllers
{
    public class AdminController : Controller

    {
        private readonly IProductsStorage productsStorage;
        private readonly IOrdersStorage ordersStorage;

        public AdminController(IProductsStorage productsStorage, IOrdersStorage ordersStorage)
        {
            this.productsStorage = productsStorage;
            this.ordersStorage = ordersStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = ordersStorage.GetAll();
            return View(orders);
        }

        public IActionResult Products()
        {
            var products = productsStorage.GetAll();
            return View(products);
        }
    }
}
