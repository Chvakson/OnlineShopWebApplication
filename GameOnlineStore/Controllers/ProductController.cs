using GameOnlineStore.Db.Repositories.Products;
using GameOnlineStore.Helpers;
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
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(product.ToProductViewModel());
        }
    }
}
