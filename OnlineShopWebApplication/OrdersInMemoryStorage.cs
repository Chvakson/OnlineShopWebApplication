using OnlineShopWebApplication.Models;
using System.Collections.ObjectModel;

namespace OnlineShopWebApplication
{
    public class OrdersInMemoryStorage : IOrdersStorage
    {
        private readonly List<Cart> orders = new List<Cart>();

        private readonly List<UserData> usersData = new List<UserData>();

        public void Add(Cart cart)
        {
            orders.Add(cart);
        }

        public void Add(UserData userData)
        {
            usersData.Add(userData);
        }
    }
}