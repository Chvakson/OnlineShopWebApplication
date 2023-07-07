using OnlineShopWebApplication.Models;
using System.Collections.ObjectModel;

namespace OnlineShopWebApplication
{
    public class OrdersInMemoryStorage : IOrdersStorage
    {
        private readonly List<Cart> orders = new List<Cart>();

        private readonly List<User> usersData = new List<User>();

        public void Add(Cart cart)
        {
            orders.Add(cart);
        }

        public void Add(User userData)
        {
            usersData.Add(userData);
        }
    }
}