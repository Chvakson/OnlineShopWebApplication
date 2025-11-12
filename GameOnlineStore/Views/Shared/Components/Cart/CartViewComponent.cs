using GameOnlineStore.Db.Models;
using GameOnlineStore.Db.Repositories.Carts;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using GameOnlineStore.Helpers;


namespace GameOnlineStore.Views.Shared.Components.CartViewComponent
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsDbRepository cartsDbRepository;

        public CartViewComponent(ICartsDbRepository cartsDbRepository)
        {
            this.cartsDbRepository = cartsDbRepository;
        }

        public IViewComponentResult Invoke()
        {
            var cart = cartsDbRepository.TryGetByUserId(Constants.UserId);
            var productCounts = cart.ToCartViewModel()?.Amount ?? 0;
            return View("Cart", productCounts);
        }
    }
}
