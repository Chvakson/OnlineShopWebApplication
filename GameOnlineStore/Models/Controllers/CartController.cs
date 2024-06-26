﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication;


namespace GameOnlineStore.Models.Controllers
{

    public class CartController : Controller
    {
        private readonly IProductsStorage productsStorage;
        private readonly ICartsStorage cartsStorage;

        public CartController(IProductsStorage productsStorage, ICartsStorage cartsStorage)
        {
            this.productsStorage = productsStorage;
            this.cartsStorage = cartsStorage;
        }

        public IActionResult Index()
        {
            var cart = cartsStorage.TryGetByUserId(Constants.UserId);
            return View(cart);
        }

        public IActionResult Add(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            cartsStorage.Add(product, Constants.UserId);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(int productId)
        {
            var product = productsStorage.TryGetById(productId);
            cartsStorage.Remove(product, Constants.UserId);

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            cartsStorage.Clear(Constants.UserId);

            return RedirectToAction("Index");
        }
    }
}
