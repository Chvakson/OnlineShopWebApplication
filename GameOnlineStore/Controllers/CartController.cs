using Microsoft.AspNetCore.Mvc;


namespace GameOnlineStore.Controllers
{

    public class CartController : Controller
    {
        private readonly IProductsStorage productsStorage;

        public CartController(IProductsStorage productsStorage)
        {
            this.productsStorage = productsStorage;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            return RedirectToAction("Index");
        }
    }
}
