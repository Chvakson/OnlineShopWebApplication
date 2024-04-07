using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Controllers
{
    public class AdminController : Controller

    {
        private readonly IProductsStorage productsStorage;
        private readonly IOrdersStorage ordersStorage;
        private readonly IRolesStorage rolesStorage;

        public AdminController(IProductsStorage productsStorage, IOrdersStorage ordersStorage, IRolesStorage rolesStorage)
        {
            this.productsStorage = productsStorage;
            this.ordersStorage = ordersStorage;
            this.rolesStorage = rolesStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = ordersStorage.GetAll();
            return View(orders);
        }

        #region Products

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
            if (!ModelState.IsValid)
            {
                return RedirectToAction("CreateNewProduct", product);
            };
            productsStorage.Add(product);
            return RedirectToAction("Products");
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
            return RedirectToAction("Products");
        }

        public IActionResult RemoveProduct(int productId)
        {
            productsStorage.Remove(productId);
            return RedirectToAction("Products");
        }

        #endregion

        #region Roles

        public IActionResult Roles()
        {
            var roles = rolesStorage.GetAll();
            return View(roles);
        }

        public IActionResult AddNewRole()
        {
            rolesStorage.Add();
            return RedirectToAction("Roles");
        }
        #endregion
    }
}
