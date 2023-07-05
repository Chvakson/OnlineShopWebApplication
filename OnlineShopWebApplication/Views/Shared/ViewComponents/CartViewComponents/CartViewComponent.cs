using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace OnlineShopWebApplication.Views.Shared.Components.CartViewComponents
{
    public class Cart : ViewComponent
    {
        private readonly ICartsStorage cartsStorage;

        public Cart(ICartsStorage cartsStorage)
        {
            this.cartsStorage = cartsStorage;
        }

        public IViewComponentResult Invoke()
        {
            var cart = cartsStorage.TryGetByUserId(Constants.UserId);
            var productCounts = cart?.Amount ?? 0;

            return View("Cart", productCounts);
        }
    }
}
