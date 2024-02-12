using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;

namespace GameOnlineStore.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IFavoriteStorage favoriteStorage;

        public FavoriteController(IProductsStorage productsStorage, IFavoriteStorage favoriteStorage)
        {
            this.productsStorage = productsStorage;
            this.favoriteStorage = favoriteStorage;
        }

        public IActionResult Index()
        {
            var favorite = favoriteStorage.TryGetByUserId(Constants.UserId);
            return View(favorite);
        }

        public IActionResult Add(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            favoriteStorage.Add(product, Constants.UserId);
            return View();
        }
    }
}
