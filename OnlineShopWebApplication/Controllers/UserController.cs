using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApplication.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
