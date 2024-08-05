using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;

namespace GameOnlineStore.Models.Controllers
{
    public class CompareProductsController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly ICompareProducts compareProductsStorage;

        public CompareProductsController(IProductsStorage productsStorage, ICompareProducts compareProductsStorage)
        {
            this.productsStorage = productsStorage;
            this.compareProductsStorage = compareProductsStorage;
        }

        public IActionResult Index()
        {
            var favorite = compareProductsStorage.TryGetByUserId(Constants.UserId);
            return View(favorite);
        }

        public IActionResult Add(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            compareProductsStorage.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            compareProductsStorage.Remove(product);
            return RedirectToAction("Index");
        }
    }
}
