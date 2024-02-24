using GameOnlineStore.Models;
using OnlineShopWebApplication;

namespace GameOnlineStore
{
    public class OrdersInMemoryStorage : IOrdersStorage
    {
        public List<Order> orders = new List<Order>();

        public List<Order> TryGetById(Guid id)
        {
            return orders.Where(order => order.Id == id).ToList();
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
