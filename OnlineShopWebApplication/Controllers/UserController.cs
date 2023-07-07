using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;

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
        public IActionResult Login(Login login)
        {
            var user = usersStorage.TryGetByLogin(login);
            if (user == null)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Register(Login login)
        {
            var user = new User
            {
                UserId = new Guid(),
                Login = login
            };
            usersStorage.Add(user);
            return Ok();
        }
    }
}
