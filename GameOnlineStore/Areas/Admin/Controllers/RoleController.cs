using GameOnlineStore.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : Controller
    {
        private readonly IRolesStorage rolesStorage;

        public RoleController(IRolesStorage rolesStorage)
        {
            this.rolesStorage = rolesStorage;
        }

        public IActionResult Index()
        {
            var roles = rolesStorage.GetAll();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Role role)
        {
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
