using GameOnlineStore.Repositories.FavoriteProducts;
using GameOnlineStore.Repositories.Products;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;

namespace GameOnlineStore.Models.Controllers
{
    public class FavoriteProductsController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IFavoriteProducts favoriteProductsStorage;

        public FavoriteProductsController(IProductsStorage productsStorage, IFavoriteProducts favoriteProductsStorage)
        {
            this.productsStorage = productsStorage;
            this.favoriteProductsStorage = favoriteProductsStorage;
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
