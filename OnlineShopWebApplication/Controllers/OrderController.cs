﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly ICartsStorage cartsStorage;
        private readonly IOrdersStorage ordersStorage;
        public OrderController(ICartsStorage cartsStorage, IOrdersStorage ordersStorage)
        {
            this.cartsStorage = cartsStorage;
            this.ordersStorage = ordersStorage;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Buy(User user)
        {
            var existingCart = cartsStorage.TryGetByUserId(Constants.UserId);
            var order = new Order
            {
                Date = DateTime.Now,
                Id = new Guid(),
                User = user,
                items = existingCart.Items,
                Status = OrderStatus.Created
                
            };
            ordersStorage.Add(order);
            cartsStorage.Clear(Constants.UserId);
            return Ok();
        }
    }
}
