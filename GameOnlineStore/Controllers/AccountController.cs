using GameOnlineStore;
using GameOnlineStore.Areas.Admin.Models;
using GameOnlineStore.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GameOnlineStore.Models.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUsersManager usersManager;

        public AccountController(IUsersManager usersManager)
        {
            this.usersManager = usersManager;
        }

        [HttpPost]
        public IActionResult SignIn([FromBody] LoginCredential loginCredential)
        {
            if (!Regex.IsMatch(loginCredential.Login, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                return BadRequest("Указан неверный адрес электронной почты");
            else if (loginCredential.Login.Length < 7 || loginCredential.Login.Length > 75)
                return BadRequest("Логин должен содержать от 7 до 75 символов");
            else
            {
                var userAccount = usersManager.TryGetByName(loginCredential.Login);
                if (userAccount == null)
                    return BadRequest("Такого пользователя не существует");
                else
                {
                    if(userAccount.Password != loginCredential.Password)
                        return BadRequest("Неверный пароль");

                }
            }
            return Ok();
        }

        [HttpPost]
        public IActionResult Register([FromBody] RegisterDetails registerDetails)
        {
            if (registerDetails.Login.Length < 7 || registerDetails.Login.Length > 75)
                return BadRequest("Логин должен содержать от 7 до 75 символов");

            if (registerDetails.Password != registerDetails.ConfirmPassword)
                return BadRequest("Пароли не совпадают");

            usersManager.Add(new UserAccount
            {
                Login = registerDetails.Login,
                Phone = registerDetails.Phone,
                Password = registerDetails.Password,
                Role = new Role("User")
            });

            return Ok();
        }
    }
}
