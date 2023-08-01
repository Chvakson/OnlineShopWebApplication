using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly IOrdersStorage ordersStorage;
        private readonly IRolesStorage rolesStorage;

        public AdminPanelController(IProductsStorage productsStorage, IOrdersStorage ordersStorage, IRolesStorage rolesStorage)
        {
            this.productsStorage = productsStorage;
            this.ordersStorage = ordersStorage;
            this.rolesStorage = rolesStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = ordersStorage.GetAll();
            return View(orders);
        }

        public IActionResult Order(Guid id)
        {
            var order = ordersStorage.TryGetByOrderId(id);
            return View(order);
        }

        public IActionResult Users()
        {
            return View();
        }

        public IActionResult Roles()
        {
            var roles = rolesStorage.GetAll();
            return View(roles);
        }

        public IActionResult RemoveRole(string name)
        {
            rolesStorage.Remove(name);
            return RedirectToAction("Roles", "AdminPanel");
        }

        public IActionResult AddRole()
        {
            return View("NewRole");
        }

        [HttpPost]
        public IActionResult AddRole(Role role)
        {
            if (rolesStorage.TryGetById(role.Name) != null) 
            {
                ModelState.AddModelError("", "Такая модель уже существует");
            }
            if(ModelState.IsValid)
            {
                var roles = rolesStorage.GetAll();
                roles.Add(role);
            }
            return RedirectToAction("Roles", "AdminPanel");
        }


        public IActionResult Products()
        {
            var products = productsStorage.GetAll();
            return View(products);
        }

        public IActionResult RemoveProduct(int productId) 
        {
            productsStorage.RemoveById(productId);
            return RedirectToAction("Products", "AdminPanel"); 
        }

        public IActionResult EditProduct(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            return View("ProductEditor", product);
        }

        [HttpPost]
        public IActionResult SaveProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = productsStorage.GetAll().FirstOrDefault(p => p.Id == product.Id);
                if (existingProduct != null)
                {
                    existingProduct.Name = product.Name;
                    existingProduct.Cost = product.Cost;
                    existingProduct.Description = product.Description;
                    existingProduct.ImgPath = product.ImgPath;
                }
                return Ok();
            }
            var newProduct = new Product(product.Name, product.Cost, product.Description, product.ImgPath);
            productsStorage.Add(newProduct);
            return Ok();
        }

        public IActionResult AddNewProduct()
        {
            return View("NewProduct");
        }
    }
}