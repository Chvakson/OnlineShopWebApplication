using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsStorage productStorage;
        private readonly IFavoriteStorage favoriteStorage;

        public HomeController(IProductsStorage productStorage, IFavoriteStorage favoriteStorage)
        {
            this.productStorage = productStorage;
            this.favoriteStorage = favoriteStorage;
        }

        public IActionResult Index()
        {
            var products = productStorage.GetAll();
            return View(products);
        }

        [HttpPost]
        public IActionResult Index(string? query)
        {
            var products = productStorage.GetAll();

            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(p => p.Name != null && p.Name.ToLower().Contains(query.ToLower())).ToList();
            }


            return View(products);
        }
    }
}