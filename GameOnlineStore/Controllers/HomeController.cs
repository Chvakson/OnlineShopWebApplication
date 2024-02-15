using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using System.Diagnostics;

namespace GameOnlineStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsStorage productsStorage;


        public HomeController(IProductsStorage productsStorage)
        {
            this.productsStorage = productsStorage;
        }

        public IActionResult Index()
        {
            var products = productsStorage.GetAll(); 
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
