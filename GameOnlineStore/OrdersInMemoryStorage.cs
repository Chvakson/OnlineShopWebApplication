using GameOnlineStore.Models;
using OnlineShopWebApplication;

namespace GameOnlineStore
{
    public class OrdersInMemoryStorage : IOrdersStorage
    {
        public List<Order> orders = new List<Order>();

        public List<Order> TryGetByUserId(string userId)
        {
            return orders.Where(order => order.UserId == userId).ToList();
        }

        public List<Order> GetAll()
        {
            return orders;
        }

        public void Add(Cart cart, UserAddress userAddress, UserContacts userContacts, string? comment)
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = Constants.UserId,
                Cart = cart,
                UserAddress = userAddress,
                UserContacts = userContacts,
                Comment = comment
            };
            orders.Add(order);
        }
    }
}
