using GameOnlineStore.Db.Repositories.Carts;
using GameOnlineStore.Db.Repositories.Products;
using GameOnlineStore.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;

namespace GameOnlineStore.Models.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IProductsDbRepository productsDbRepositoty;
        private readonly ICartsDbRepository cartsDbRepository;

        public CartController(IProductsDbRepository productsDbRepositoty, ICartsDbRepository cartsDbRepository)
        {
            this.productsDbRepositoty = productsDbRepositoty;
            this.cartsDbRepository = cartsDbRepository;
        }

        public IActionResult Index()
        {
            var cart = cartsDbRepository.TryGetByUserId(Constants.UserId);
            return View(cart.ToCartViewModel());
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsDbRepositoty.TryGetById(productId);
            if (product == null)
            {
                return RedirectToAction("Index", "Home");
            }

            cartsDbRepository.Add(product, Constants.UserId);
            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            var product = productsDbRepositoty.TryGetById(productId);
            if (product != null)
            {
                cartsDbRepository.Remove(product, Constants.UserId);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            cartsDbRepository.Clear(Constants.UserId);
            return RedirectToAction("Index");
        }
    }
}
