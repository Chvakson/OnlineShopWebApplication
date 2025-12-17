using GameOnlineStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller

    {
        public IActionResult Index()
        {
            // Если пользователь не авторизован, показываем модальное окно
            if (!User.Identity.IsAuthenticated)
            {
                ViewBag.ShowLoginModal = true;
            }

            return View();
        }

        // Пример защищенной страницы
        [Authorize]
        public IActionResult ProtectedPage()
        {
            return View();
        }
    }
}
