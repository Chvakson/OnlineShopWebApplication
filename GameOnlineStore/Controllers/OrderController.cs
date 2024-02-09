using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
