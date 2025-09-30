using GameOnlineStore.Repositories.CompareProducts;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using GameOnlineStore.Db.Repositories.Products;

namespace GameOnlineStore.Models.Controllers
{
    public class CompareProductsController : Controller
    {
        private readonly IProductsDbRepository productsDbRepository;
        private readonly ICompareProducts compareProductsStorage;

        public CompareProductsController(IProductsDbRepository productsDbRepository, ICompareProducts compareProductsStorage)
        {
            this.productsDbRepository = productsDbRepository;
            this.compareProductsStorage = compareProductsStorage;
        }

        public IActionResult Index()
        {
            var favorite = compareProductsStorage.TryGetByUserId(Constants.UserId);
            return View(favorite);
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
           // compareProductsStorage.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
           // compareProductsStorage.Remove(product);
            return RedirectToAction("Index");
        }
    }
}
