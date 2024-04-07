using GameOnlineStore.Models;
using OnlineShopWebApplication;

namespace GameOnlineStore
{
    public class OrdersInMemoryStorage : IOrdersStorage
    {
        public List<Order> Orders = new List<Order>();

        public Order? TryGetById(Guid id)
        {
            return Orders.FirstOrDefault(order => order.Id == id);
        }

        public List<Order> GetAll()
        {
            return Orders;
        }

        public void Add(Order order)
        {
            Orders.Add(order);
        }
    }
}
