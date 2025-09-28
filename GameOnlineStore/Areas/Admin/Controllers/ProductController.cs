using GameOnlineStore.Db.Models;
using GameOnlineStore.Db.Repositories;
using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductsDbRepository productsDbRepository;

        public ProductController(IProductsDbRepository productsStorage)
        {
            this.productsDbRepository = productsStorage;
        }

        public IActionResult Index()
        {
            var productsDb = productsDbRepository.GetAll();
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Create", product);
            };

            var productDb = new Product
            {
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImgFileName = product.ImgFileName
            };

            productsDbRepository.Add(productDb);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(Guid productId)
        {
            var product = productsDbRepository.TryGetById(productId);
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImgFileName = product.ImgFileName,
            };
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Update(ProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Edit", product);
            };

            var productDb = new Product
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImgFileName = product.ImgFileName
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
