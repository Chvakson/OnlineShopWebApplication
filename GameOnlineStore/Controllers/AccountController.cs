using GameOnlineStore.Areas.Admin.Models;
using GameOnlineStore.Db.Models;
using GameOnlineStore.Models.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace GameOnlineStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;


        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignIn(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody] LoginCredential loginCredential)
        {
            // Валидация email
            if (!Regex.IsMatch(loginCredential.Login, @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"))
                return Json(new { success = false, message = "Указан неверный адрес электронной почты" });

            // Поиск пользователя
            var user = await userManager.FindByEmailAsync(loginCredential.Login);
            if (user == null)
                return Json(new { success = false, message = "Пользователь не найден" });

            // Вход
            var result = await signInManager.PasswordSignInAsync(
                user,
                loginCredential.Password,
                loginCredential.RememberMe ?? false,
                lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("Index", "Home")
                });
            }

            if (result.IsLockedOut)
            {
                return Json(new { success = false, message = "Аккаунт заблокирован" });
            }

            return Json(new
            {
                success = false,
                message = "Неверный пароль"
            });
        }


        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDetails registerDetails)
        {
            if (registerDetails.Login.Length < 7 || registerDetails.Login.Length > 75)
                return BadRequest("Логин должен содержать от 7 до 75 символов");

            if (registerDetails.Password != registerDetails.ConfirmPassword)
                return BadRequest("Пароли не совпадают");

            // Создаем пользователя через UserManager
            var user = new User
            {
                UserName = registerDetails.Login,
                Email = registerDetails.Login,
                PhoneNumber = registerDetails.Phone
            };

            var result = await userManager.CreateAsync(user, registerDetails.Password);

            if (result.Succeeded)
            {
                // Добавляем роль "Пользователь"
                await userManager.AddToRoleAsync(user, "Пользователь");

                // Автоматически логиним после регистрации
                await signInManager.SignInAsync(user, isPersistent: false);

                return Ok(new { message = "Регистрация успешна" });
            }

            // Возвращаем ошибки Identity
            return BadRequest(string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
