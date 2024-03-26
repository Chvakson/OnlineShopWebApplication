using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Controllers
{
    public class AdminController : Controller

    {
        private readonly IProductsStorage productsStorage;
        private readonly IOrdersStorage ordersStorage;

        public AdminController(IProductsStorage productsStorage, IOrdersStorage ordersStorage)
        {
            this.productsStorage = productsStorage;
            this.ordersStorage = ordersStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(int productId, Product product)
        {
            var editingProduct = productsStorage.TryGetById(productId);
            if (editingProduct != null)
            {
                editingProduct.Name = product.Name;
                editingProduct.Cost = product.Cost;
                editingProduct.Description = product.Description;
            }
            return RedirectToAction("Products");
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = ordersStorage.GetAll();
            return View(orders);
        }

        public IActionResult Products()
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
            if (product != null)
            {
                var products = productsStorage.GetAll();
                products.Add(product);
            }
            return RedirectToAction("Products");
        }

        public IActionResult EditProduct(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            return View(product);
        }

        public IActionResult RemoveProduct(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            var products = productsStorage.GetAll();
            products.Remove(product);
            return RedirectToAction("Products");
        }
    }
}
