using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using System.Diagnostics;

namespace GameOnlineStore.Models.Controllers
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

        [HttpPost]
        public IActionResult Index(string? query)
        {
            var products = productsStorage.GetAll();

            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(query.ToLower())).ToList();
            }


            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
