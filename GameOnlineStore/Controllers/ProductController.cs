using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsStorage productsStorage;

        public IActionResult Index(int? productId)
        {
            var product = productsStorage.GetByProductId(productId);
            return View(product);
        }
    }
}
