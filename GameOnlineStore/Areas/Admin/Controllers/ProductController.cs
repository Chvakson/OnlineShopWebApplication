using GameOnlineStore.Models;
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

        public IActionResult CreateNewProduct()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNewProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateNewProduct", product);
            };
            productsStorage.Add(product);
            return RedirectToAction("Index");
        }

        public IActionResult EditProduct(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            return View(product);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("EditProduct", product);
            };
            productsStorage.Update(product);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveProduct(int productId)
        {
            productsStorage.Remove(productId);
            return RedirectToAction("Index");
        }

    }
}
