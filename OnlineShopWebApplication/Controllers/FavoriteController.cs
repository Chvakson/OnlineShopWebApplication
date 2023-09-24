using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IFavoriteStorage favoriteStorage;
        private readonly IProductsStorage productsStorage;

        public FavoriteController(IFavoriteStorage favoriteStorage, IProductsStorage productsStorage)
        {
            this.favoriteStorage = favoriteStorage;
            this.productsStorage = productsStorage;
        }

        public IActionResult Index()
        {
            var products = favoriteStorage.GetAll();
            return View(products);
        }

        [HttpPost("/api/favorite/add/{productId}")]
        public IActionResult Add(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            product.IsFavorite = true;
            favoriteStorage.Add(product);
            return Ok();
        }
    }
}
