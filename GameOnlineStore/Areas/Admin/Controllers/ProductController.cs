using GameOnlineStore.Models;
using GameOnlineStore.Repositories.Products;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsStorage productsStorage;

        public ProductController(IProductsStorage productsStorage)
        {
            this.productsStorage = productsStorage;
        }

        public IActionResult Index()
        {
            var products = productsStorage.GetAll();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", product);
            };
            productsStorage.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            return View(product);
        }

        [HttpPost]
        public IActionResult Update(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", product);
            };
            productsStorage.Update(product);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            productsStorage.Remove(productId);
            return RedirectToAction("Index");
        }

    }
}
