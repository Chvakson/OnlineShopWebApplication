using GameOnlineStore.Db.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Models.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsDbRepository productsRepository;

        public ProductController(IProductsDbRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IActionResult Index(Guid? productId)
        {
            var product = productsRepository.TryGetById(productId);
            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost,
                Description = product.Description,
                ImgPath = product.ImgPath,
            };
            return View(productViewModel);
        }
    }
}
