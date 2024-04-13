using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrdersStorage ordersStorage;

        public OrderController(IOrdersStorage ordersStorage)
        {
            this.ordersStorage = ordersStorage;
        }

        public IActionResult Index()
        {
            var orders = ordersStorage.GetAll();
            return View(orders);
        }

        public IActionResult Details(Guid id)
        {
            var existingOrder = ordersStorage.TryGetById(id);

            return View(existingOrder);
        }

        public IActionResult UpdateOrderStatus(Guid id, OrderStatus status)
        {
            ordersStorage.UpdateStatus(id, status);

            return RedirectToAction("Index");
        }
    }
}
