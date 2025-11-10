using GameOnlineStore.Db.Repositories.Orders;
using GameOnlineStore.Helpers;
using GameOnlineStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrdersDbRepository ordersDbRepository;

        public OrderController(IOrdersDbRepository ordersStorage)
        {
            this.ordersDbRepository = ordersStorage;
        }

        public IActionResult Index()
        {
            var orders = ordersDbRepository.GetAll();
            
            return View(Mapping.ToOrderViewModels(orders));
        }

        public IActionResult Details(Guid id)
        {
            var existingOrder = ordersDbRepository.TryGetById(id);
            if (existingOrder != null) {
                return View(Mapping.ToOrderViewModel(existingOrder));
            }

            return RedirectToAction("Index");
        }

        public IActionResult UpdateOrderStatus(Guid id, int status)
        {
            ordersDbRepository.UpdateStatus(id, status);

            return RedirectToAction("Index");
        }
    }
}
