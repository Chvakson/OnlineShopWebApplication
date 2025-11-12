using GameOnlineStore.Helpers;
using Microsoft.AspNetCore.Mvc;
using GameOnlineStore.Db.Repositories.Products;

namespace GameOnlineStore.Models.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsDbRepository productsRepository;

        public HomeController(IProductsDbRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IActionResult Index()
        {
            var productsDb = productsRepository.GetAll();
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in productsDb)
            {
                productViewModels.Add(product.ToProductViewModel());
            }
            return View(productViewModels);
        }

        [HttpPost]
        public IActionResult Index(string? query)
        {
            var productsDb = productsRepository.GetAll();
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in productsDb)
            {
                productViewModels.Add(product.ToProductViewModel());
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
                productViewModels = productViewModels.Where(p => p.Name != null && p.Name.ToLower().Contains(query.ToLower())).ToList();
            }


            return View(productViewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
