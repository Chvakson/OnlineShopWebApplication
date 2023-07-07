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
    }
}