using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;
using System.Diagnostics;

namespace OnlineShopWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductsStorage productStorage;

        public HomeController()
        {
            productStorage = new ProductsStorage();
        }
        public IActionResult Index()
        {
            var products = productStorage.GetAll();

            return View(products);
        }
    }
}