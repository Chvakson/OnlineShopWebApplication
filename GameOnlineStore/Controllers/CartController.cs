using GameOnlineStore.Repositories.Carts;
using GameOnlineStore.Db.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;


namespace GameOnlineStore.Models.Controllers
{

    public class CartController : Controller
    {
        private readonly IProductsDbRepository productsDbRepositoty;
        private readonly ICartsStorage cartsStorage;

        public CartController(IProductsDbRepository productsDbRepositoty, ICartsStorage cartsStorage)
        {
            this.productsDbRepositoty = productsDbRepositoty;
            this.cartsStorage = cartsStorage;
        }

        public IActionResult Index()
        {
            var cart = cartsStorage.TryGetByUserId(Constants.UserId);
            return View(cart);
        }

        public IActionResult Add(Guid productId)
        {
            var product = productsDbRepositoty.TryGetById(productId);
            //cartsStorage.Add(product, Constants.UserId);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(Guid productId)
        {
            var product = productsDbRepositoty.TryGetById(productId);
            //cartsStorage.Remove(product, Constants.UserId);

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            cartsStorage.Clear(Constants.UserId);

            return RedirectToAction("Index");
        }
    }
}
