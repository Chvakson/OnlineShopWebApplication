using GameOnlineShop.Repositories.ComparedProducts;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using GameOnlineShop.Db.Repositories.Products;
using GameOnlineShop.Helpers;

namespace GameOnlineShop.Models.Controllers
{
    public class ComparedController : Controller
    {
        private readonly IProductsDbRepository productsDbRepository;
        private readonly IComparedDbRepository comparedDbRepository;

        public ComparedController(IProductsDbRepository productsDbRepository, IComparedDbRepository comparedDbRepository)
        {
            this.productsDbRepository = productsDbRepository;
            this.comparedDbRepository = comparedDbRepository;
        }

        public IActionResult Index()
        {
            var products = comparedDbRepository.GetAll(Constants.UserId);
            return View(products.ToProductViewModels());
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
            comparedDbRepository.Add(Constants.UserId, product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
            comparedDbRepository.Remove(Constants.UserId, productId);
            return RedirectToAction("Index");
        }
    }
}
