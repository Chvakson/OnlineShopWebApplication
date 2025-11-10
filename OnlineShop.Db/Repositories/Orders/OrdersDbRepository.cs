using GameOnlineStore.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOnlineStore.Db.Repositories.Orders
{
    public class OrdersDbRepository : IOrdersDbRepository
    {
        private ApplicationContext context;

        public OrdersDbRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Order? TryGetById(Guid orderId)
        {
            return context.Orders
            .Include(o => o.UserDeliveryInfo)
            .ThenInclude(udi => udi.Address) // Включаем адрес
            .Include(o => o.Items)
            .ThenInclude(i => i.Product) // Включаем продукты в элементах корзины
            .FirstOrDefault(o => o.Id == orderId); 
        }

        public List<Order> GetAll()
        {
            return context.Orders
                .Include(o => o.UserDeliveryInfo)
                .ThenInclude(d => d.Address)
                .Include(o => o.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public void Add(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
        }

        public void UpdateStatus(Guid orderId, int newStatusId)
        {
            var existingOrder = context.Orders.FirstOrDefault(o => o.Id == orderId);
            if (existingOrder != null)
            {
                existingOrder.Status = (OrderStatus)newStatusId;
                context.SaveChanges();
            }
        }

    }
}
