using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
