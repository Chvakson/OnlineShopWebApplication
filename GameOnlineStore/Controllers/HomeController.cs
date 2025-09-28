using GameOnlineStore.Models;
using GameOnlineStore.Db.Repositories;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using System.Diagnostics;

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
                ProductViewModel productViewModel = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    ImgFileName = product.ImgFileName,
                };
                productViewModels.Add(productViewModel);
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
                ProductViewModel productViewModel = new ProductViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Cost = product.Cost,
                    Description = product.Description,
                    ImgFileName = product.ImgFileName,
                };
                productViewModels.Add(productViewModel);
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
