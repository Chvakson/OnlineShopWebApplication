using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Models.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsStorage productsStorage;

        public ProductController(IProductsStorage productsStorage)
        {
            this.productsStorage = productsStorage;
        }

        public IActionResult Index(int? productId)
        {

            var product = productsStorage.TryGetById(productId);
            return View(product);
        }
    }
}
