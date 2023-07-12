using OnlineShopWebApplication.Models;
using System.Collections.ObjectModel;

namespace OnlineShopWebApplication
{
    public class OrdersInMemoryStorage : IOrdersStorage
    {
        private readonly List<Order> orders = new List<Order>();

        public void Add(Order order)
        {
            orders.Add(order);
        }

        public List<Order> GetAll()
        {
            return orders;
        }

        public Order TryGetByOrderId(Guid id)
        {
            return orders.FirstOrDefault(order => order.Id == id);
        }
    }
}