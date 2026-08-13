using GameOnlineShop.Repositories.FavoriteProducts;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using GameOnlineShop.Db.Repositories.Products;
using GameOnlineShop.Helpers;

namespace GameOnlineShop.Models.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IProductsDbRepository productsDbRepository;
        private readonly IFavoriteDbRepository favoriteDbRepository;

        public FavoriteController(IProductsDbRepository productsDbRepository, IFavoriteDbRepository favoriteDbRepository)
        {
            this.productsDbRepository = productsDbRepository;
            this.favoriteDbRepository = favoriteDbRepository;
        }

        public IActionResult Index()
        {
            var products = favoriteDbRepository.GetAll(Constants.UserId);
            return View(products.ToProductViewModels());
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
            favoriteDbRepository.Add(Constants.UserId, product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
            favoriteDbRepository.Remove(Constants.UserId, productId);
            return RedirectToAction("Index");
        }
    }
}
