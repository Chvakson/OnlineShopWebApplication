using GameOnlineStore.Areas.Admin.Models;
using GameOnlineStore.Db.Models;
using GameOnlineStore.Repositories.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRolesStorage rolesStorage;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public RoleController(IRolesStorage rolesStorage, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.rolesStorage = rolesStorage;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Role role)
        {
            var user = (User)User.Identities;
            //userManager.AddToRoleAsync(User, role.Name);
            if (rolesStorage.TryGetByName(role.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует");
            }
            if (ModelState.IsValid)
            {
                rolesStorage.Add(role);
                return RedirectToAction("Index");
            }

            return View("Create", role);
        }

        public IActionResult Remove(string name)
        {
            rolesStorage.Remove(name);
            return RedirectToAction("Index");
        }
    }
}
