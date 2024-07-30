using GameOnlineStore.Areas.Admin.Models;
using GameOnlineStore.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Data;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IUsersManager usersManager;
        private readonly IRolesStorage rolesStorage;

        public UserController(IUsersManager usersManager, IRolesStorage rolesStorage)
        {
            this.usersManager = usersManager;
            this.rolesStorage = rolesStorage;
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
        
        public IActionResult Edit(string login) 
        {
            EditUser editUser = new() { PrevLogin = login };
            return View(editUser);
        }

        [HttpPost]
        public IActionResult Edit(EditUser editUser)
        {
            if(ModelState.IsValid)
            {
                usersManager.Edit(editUser.PrevLogin, editUser.NewLogin, editUser.Phone);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Edit));
        }
        
        public IActionResult Remove(string login) 
        {
            usersManager.Remove(login);
            return RedirectToAction("Index");
        }

        public IActionResult Permissions(string login)
        {
            var roles = rolesStorage.GetAll();
            var availableUserRole = usersManager.TryGetByName(login).Roles;
            Permissions permissions = new()
            {
                Login = login,
                Roles = availableUserRole,
            };
            ViewBag.Roles = roles;
            return View(permissions);
        }

        [HttpPost]
        public IActionResult Permissions(Permissions permissions)
        {
            if (ModelState.IsValid)
            {
                usersManager.GetPermissions(permissions.Login, permissions.Roles);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Permissions));
        }
    }
}

