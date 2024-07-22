using GameOnlineStore.Areas.Admin.Models;
using GameOnlineStore.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUsersManager usersManager;

        public UserController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        public IActionResult Index()
        {
            var userAccounts = usersManager.GetAll();
            return View(userAccounts);
        }

        public IActionResult Details(string login)
        {
            var userAccount = usersManager.TryGetByName(login);
            return View(userAccount);
        }

        public IActionResult ChangePassword(string login) 
        {
            ChangePassword changePassword = new() { Login = login };
            return View(changePassword);
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePassword changePassword)
        {
            if (changePassword.Login == changePassword.Password)
                ModelState.AddModelError("", "Логин и пароль не должны совпадать!");

            if(ModelState.IsValid)
            {
                usersManager.ChangePassword(changePassword.Login, changePassword.Password);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(ChangePassword));
        }
    }
}
