using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;

namespace GameOnlineStore.Controllers
{
    public class FavoriteProductsController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IProductsCollection favoriteProductsStorage;

        public FavoriteProductsController(IProductsStorage productsStorage, IProductsCollection favoriteStorage)
        {
            this.productsStorage = productsStorage;
            this.favoriteProductsStorage = favoriteStorage;
        }

        public IActionResult Index()
        {
            var favorite = favoriteProductsStorage.TryGetByUserId(Constants.UserId);
            return View(favorite);
        }

        public IActionResult Add(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            favoriteProductsStorage.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            favoriteProductsStorage.Remove(product);
            return RedirectToAction("Index");
        }
    }
}
