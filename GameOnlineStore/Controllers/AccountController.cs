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
        public IActionResult SignIn([FromBody] LoginCredential loginCredential)
        {
            if (loginCredential.Login.Length < 5 || loginCredential.Login.Length > 50)
                return BadRequest("Логин должен содержать от 5 до 50 символов");

            return Ok();
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterDetails registerDetails)
        {
            if (registerDetails.NewLogin.Length < 5 || registerDetails.NewLogin.Length > 50)
                return BadRequest("Логин должен содержать от 5 до 50 символов");

            if (registerDetails.NewPassword != registerDetails.ConfirmPassword)
                return BadRequest("Пароли не совпадают");

            usersStorage.RegisterNewUser(registerDetails);

            return Ok();
        }
    }
}
