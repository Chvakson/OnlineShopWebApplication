using GameOnlineStore;
using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GameOnlineStore.Models.Controllers
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
            if (!Regex.IsMatch(loginCredential.Login, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                return BadRequest("Указан неверный адрес электронной почты");
            else if (loginCredential.Login.Length < 7 || loginCredential.Login.Length > 75)
                return BadRequest("Логин должен содержать от 7 до 75 символов");
            else
                if (!usersStorage.IsUserCredentialsValid(loginCredential))
                return BadRequest("Указан неверный логин или пароль");

            return Ok();
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterDetails registerDetails)
        {
            if (registerDetails.Login.Length < 7 || registerDetails.Login.Length > 75)
                return BadRequest("Логин должен содержать от 7 до 75 символов");

            if (registerDetails.Password != registerDetails.ConfirmPassword)
                return BadRequest("Пароли не совпадают");

            usersStorage.RegisterNewUser(registerDetails);

            return Ok();
        }
    }
}
