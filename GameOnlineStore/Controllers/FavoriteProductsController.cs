using GameOnlineStore.Repositories.FavoriteProducts;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using GameOnlineStore.Db.Repositories.Products;

namespace GameOnlineStore.Models.Controllers
{
    public class FavoriteProductsController : Controller
    {
        private readonly IProductsDbRepository productsDbRepository;
        private readonly IFavoriteProducts favoriteProductsStorage;

        public FavoriteProductsController(IProductsDbRepository productsDbRepository, IFavoriteProducts favoriteProductsStorage)
        {
            this.productsDbRepository = productsDbRepository;
            this.favoriteProductsStorage = favoriteProductsStorage;
        }

        public IActionResult Index()
        {
            var favorite = favoriteProductsStorage.TryGetByUserId(Constants.UserId);
            return View(favorite);
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
            favoriteProductsStorage.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
            favoriteProductsStorage.Remove(product);
            return RedirectToAction("Index");
        }
    }
}
