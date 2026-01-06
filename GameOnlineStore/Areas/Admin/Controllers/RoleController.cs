using GameOnlineStore.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
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
        public async Task<IActionResult> Add(Role role)
        {
            //userManager.AddToRoleAsync(User, role.Name);
            if (await roleManager.FindByNameAsync(role.Name) != null)
            {
                ModelState.AddModelError("", "Такая роль уже существует");
            }
            if (ModelState.IsValid)
            {
                await roleManager.CreateAsync(new IdentityRole(role.Name));
                return RedirectToAction("Index");
            }

            return View("Create", role);
        }

        public async Task<IActionResult> Remove(string name)
        {
            var existingRole = await roleManager.FindByNameAsync(name);
            if(existingRole != null)
                await roleManager.DeleteAsync(existingRole);
            return RedirectToAction("Index");
        }
    }
}
