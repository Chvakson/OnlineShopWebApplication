using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductsStorage ProductsStorage;
        public CartController()
        {
            ProductsStorage = new ProductsStorage();
        }
        public IActionResult Index()
        {
            var cart = CartsStorage.TryGetByUserId(Constants.UserId);
            return View(cart);
        }

        public IActionResult Add(int productId)
        {
            var product = ProductsStorage.TryGetById(productId);
            CartsStorage.Add(product, Constants.UserId);

            return RedirectToAction("Index");
        }
    }
}
