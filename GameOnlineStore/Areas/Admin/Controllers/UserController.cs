using GameOnlineStore.Areas.Admin.Models;
using GameOnlineStore.Db;
using GameOnlineStore.Db.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly ApplicationContext context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public UserController(ApplicationContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var userAccounts = userManager.Users;
            return View(userAccounts.ToList());
        }

        public async Task<IActionResult> Details(string login)
        {
            var userAccount = await userManager.FindByNameAsync(login);
            var userRoles = await userManager.GetRolesAsync(userAccount);

            var userRoleName = userRoles.FirstOrDefault() ?? "Нет роли";

            ViewBag.UserRole = userRoleName;
            return View(userAccount);
        }

        public IActionResult ChangePassword(string login) 
        {
            ChangePassword changePassword = new() { Login = login };
            return View(changePassword);
        }

        //[HttpPost]
        //public IActionResult ChangePassword(User user)
        //{
        //    if (changePassword.Login == changePassword.Password)
        //        ModelState.AddModelError("", "Логин и пароль не должны совпадать!");

        //    if(ModelState.IsValid)
        //    {
        //        userManager.ChangePasswordAsync(user, user.pa);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return RedirectToAction(nameof(ChangePassword));
        //}

        public IActionResult Edit(string login)
        {
            EditUser editUser = new() { PrevLogin = login };
            return View(editUser);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUser editUser)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await userManager.FindByEmailAsync(editUser.PrevLogin);
                userManager.ChangePhoneNumberAsync(existingUser, editUser.Phone, default);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Edit));
        }

        public async Task<IActionResult> Remove(string login)
        {
            var existingUser = await userManager.FindByNameAsync(login);
            await userManager.DeleteAsync(existingUser);
            return RedirectToAction("Index");
        }

        //public IActionResult Permissions(string login)
        //{
        //    var roles = rolesStorage.GetAll();
        //    var existingPermissions = usersManager.GetPermissions(login);
        //    var availableUserRole = usersManager.TryGetByName(login).Roles;
        //    ViewBag.Roles = roles;
        //    return View(existingPermissions);
        //}

        //[HttpPost]
        //public IActionResult Permissions([FromBody] Permissions permissions)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        usersManager.GivePermissions(permissions.Login, permissions.RoleNames);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return RedirectToAction(nameof(Permissions));
        //}
    }
}

