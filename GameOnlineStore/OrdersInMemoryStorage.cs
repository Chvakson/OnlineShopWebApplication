using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public class OrdersInMemoryStorage : IOrdersStorage
    {
        public List<Order> orders = new List<Order>();

        public List<Order> TryGetByUserId(string userId)
        {
            return orders.Where(order => order.UserId == userId).ToList();
        }

        public void Add(Cart cart, string userId)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Cart = cart

            };
            orders.Add(order);
        }
    }
}
