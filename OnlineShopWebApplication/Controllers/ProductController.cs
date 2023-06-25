﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using OnlineShopWebApplication.Models;
using System.Xml.Linq;

namespace OnlineShopWebApplication.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductsStorage productStorage;

        public ProductController(ProductsStorage productsStorage)
        {
            productStorage = productsStorage;
        }


        public IActionResult Index(int? id)
        {
            var pruduct = productStorage.TryGetById(id);
            return View(pruduct);
        }
    }
}
