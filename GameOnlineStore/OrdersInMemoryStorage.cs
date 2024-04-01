using GameOnlineStore.Models;
using OnlineShopWebApplication;

namespace GameOnlineStore
{
    public class OrdersInMemoryStorage : IOrdersStorage
    {
        public List<Order> orders = new List<Order>();

        public Order? TryGetById(Guid id)
        {
            return orders.FirstOrDefault(order => order.Id == id);
        }

        public List<Order> GetAll()
        {
            return orders;
        }

        public void Add(Order order)
        {
            orders.Add(order);
        }
    }
}
