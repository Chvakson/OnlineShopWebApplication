using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication.Controllers
{
    public class AdminController : Controller
    {
        private readonly Admin admin;

        public AdminController(Admin admin) 
        { 
            this.admin = admin;
        }

        public IActionResult Index()
        {
            return View(admin);
        }
    }
}
