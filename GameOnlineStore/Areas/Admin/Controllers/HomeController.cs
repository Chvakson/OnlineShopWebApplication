using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller

    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
