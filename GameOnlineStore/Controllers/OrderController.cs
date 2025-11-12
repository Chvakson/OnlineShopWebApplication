using GameOnlineStore.Db.Repositories.Carts;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;
using GameOnlineStore.Helpers;
using GameOnlineStore.Db.Repositories.Orders;

namespace GameOnlineStore.Models.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersDbRepository ordersDbRepository;
        private readonly ICartsDbRepository cartsDbRepository;
        public OrderController(IOrdersDbRepository ordersDbRepository, ICartsDbRepository cartsDbRepository)
        {
            this.ordersDbRepository = ordersDbRepository;
            this.cartsDbRepository = cartsDbRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buy(UserDeliveryInfoViewModel userDeliveryInfoViewModel)
        {
            if (ModelState.IsValid)
            {
                var existingCart = cartsDbRepository.TryGetByUserId(Constants.UserId);
                var existingCartViewModel = existingCart.ToCartViewModel;

                var orderViewModel = new OrderViewModel
                {
                    UserDeliveryInfo = userDeliveryInfoViewModel,
                    Items = existingCart.Items.ToCartItemViewModels()
                };
                var orderDb = orderViewModel.ToOrderDbModel(existingCart);
                ordersDbRepository.Add(orderDb);
                cartsDbRepository.Clear(Constants.UserId);

                return View();
            }
            return View();
        }
    }
}
