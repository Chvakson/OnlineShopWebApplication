using GameOnlineShop.Db.Repositories.Carts;
using GameOnlineShop.Db.Repositories.Orders;
using GameOnlineShop.Helpers;
using GameOnlineShop.Services;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;

namespace GameOnlineShop.Models.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersDbRepository ordersDbRepository;
        private readonly ICartsDbRepository cartsDbRepository;
        private readonly IOrderEmailService orderEmailService;

        public OrderController(
            IOrdersDbRepository ordersDbRepository,
            ICartsDbRepository cartsDbRepository,
            IOrderEmailService orderEmailService)
        {
            this.ordersDbRepository = ordersDbRepository;
            this.cartsDbRepository = cartsDbRepository;
            this.orderEmailService = orderEmailService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Buy(UserDeliveryInfoViewModel userDeliveryInfoViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", userDeliveryInfoViewModel);
            }

            var existingCart = cartsDbRepository.TryGetByUserId(Constants.UserId);
            if (existingCart?.Items == null || existingCart.Items.Count == 0)
            {
                return RedirectToAction("Index", "Cart");
            }

            var orderViewModel = new OrderViewModel
            {
                UserDeliveryInfo = userDeliveryInfoViewModel,
                Items = existingCart.Items.ToCartItemViewModels()
            };
            var orderDb = orderViewModel.ToOrderDbModel(existingCart);
            ordersDbRepository.Add(orderDb);

            orderViewModel.Id = orderDb.Id;
            orderViewModel.CreatedDateTime = orderDb.CreatedDateTime;

            var emailResult = await orderEmailService.SendOrderConfirmationAsync(orderViewModel);
            cartsDbRepository.Clear(Constants.UserId);

            ViewBag.EmailSent = emailResult.Sent;
            ViewBag.EmailError = emailResult.Error;
            ViewBag.CustomerEmail = userDeliveryInfoViewModel.Email;
            return View();
        }
    }
}
