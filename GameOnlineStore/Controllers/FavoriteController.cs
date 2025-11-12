using GameOnlineStore.Repositories.FavoriteProducts;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using GameOnlineStore.Db.Repositories.Products;
using GameOnlineStore.Helpers;

namespace GameOnlineStore.Models.Controllers
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
