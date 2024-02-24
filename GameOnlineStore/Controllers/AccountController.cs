using GameOnlineStore;
using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersStorage usersStorage;
        public AccountController(IUsersStorage usersStorage)
        {
            this.usersStorage = usersStorage;
        }

        [HttpPost]
        public IActionResult Login(Login loginInfo)
        {
            var existingUser = usersStorage.Login(loginInfo);
            if (existingUser == null)
            {
                return NotFound();
            }
            return RedirectToAction("Index", "Home");
            //return Ok();
        }

        [HttpPost]
        public IActionResult Register(Register registerInfo)
        {
            if(registerInfo.Password == registerInfo.ConfirmPassword) {
                usersStorage.Register(registerInfo);
            }
            return RedirectToAction("Index", "Home");
            //return Ok();
        }
    }
}
