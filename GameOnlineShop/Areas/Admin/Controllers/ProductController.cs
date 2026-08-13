using GameOnlineShop.Db.Models;
using GameOnlineShop.Db.Repositories.Products;
using GameOnlineShop.Helpers;
using GameOnlineShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsDbRepository productsDbRepository;

        public ProductController(IProductsDbRepository productsDbRepository)
        {
            this.productsDbRepository = productsDbRepository;
        }

        public IActionResult Index()
        {
            var productsDb = productsDbRepository.GetAll();
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in productsDb) 
            {
                productViewModels.Add(product.ToProductViewModel());
            }
            return View(productViewModels);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", productViewModel);
            };

            var productDb = new Product
            {
                Name = productViewModel.Name,
                Cost = productViewModel.Cost,
                Description = productViewModel.Description,
                ImgFileName = productViewModel.ImgFileName,
                Genre = productViewModel.Genre,
                Developer = productViewModel.Developer,
                ReleaseYear = productViewModel.ReleaseYear
            };

            productsDbRepository.Add(productDb);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
            return View(product.ToProductViewModel());
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", productViewModel);
            };

            var productDb = new Product
            {
                Id = productViewModel.Id,
                Name = productViewModel.Name,
                Cost = productViewModel.Cost,
                Description = productViewModel.Description,
                ImgFileName = productViewModel.ImgFileName,
                Genre = productViewModel.Genre,
                Developer = productViewModel.Developer,
                ReleaseYear = productViewModel.ReleaseYear
            };

            productsDbRepository.Update(productDb);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            productsDbRepository.Remove(productId);
            return RedirectToAction("Index");
        }

    }
}
