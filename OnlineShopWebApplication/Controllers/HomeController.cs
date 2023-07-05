using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;
using System.Diagnostics;

namespace OnlineShopWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsStorage productStorage;

        public HomeController(IProductsStorage productStorage)
        {
            this.productStorage = productStorage;
        }
        public IActionResult Index()
        {
            var products = productStorage.GetAll();
            return View(products);
        }
    }
}