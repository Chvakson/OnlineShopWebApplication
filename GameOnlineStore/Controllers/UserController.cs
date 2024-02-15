using GameOnlineStore;
using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly IUsersStorage usersStorage;
        public UserController(IUsersStorage usersStorage)
        {
            this.usersStorage = usersStorage;
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            var existingUser = usersStorage.Login(user);
            if (existingUser == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
            //return Ok();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            usersStorage.Register(user);
            return RedirectToAction("Index", "Home");
            //return Ok();
        }
    }
}
