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
        public IActionResult Index(User userData)
        {
            var user = usersStorage.TryGetByUserId(userData.UserId);
            usersStorage.Add(user);
            return Ok();
        }
    }
}
