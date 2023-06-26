using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IFavoriteStorage favoriteStorage;

        public IActionResult Index()
        {
            return View();
        }

        public void Add(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            favoriteStorage.Add(product);
        }
    }
}
