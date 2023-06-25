using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApplication.Controllers
{
    public class CartController : Controller
    {
        private readonly ProductsStorage productsStorage;
        private readonly CartsStorage cartsStorage;
        public CartController(ProductsStorage productsStorage, CartsStorage cartsStorage)
        {
            this.productsStorage = productsStorage;
            this.cartsStorage = cartsStorage;
        }
        public IActionResult Index()
        {
            var cart = cartsStorage.TryGetByUserId(Constants.UserId);
            return View(cart);
        }

        public IActionResult Add(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            cartsStorage.Add(product, Constants.UserId);

            return RedirectToAction("Index");
        }
    }
}
