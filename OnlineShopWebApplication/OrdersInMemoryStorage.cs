using OnlineShopWebApplication.Models;
using System.Collections.ObjectModel;

namespace OnlineShopWebApplication
{
    public class OrdersInMemoryStorage : IOrdersStorage
    {
        private readonly List<Cart> orders = new List<Cart>();

        public void Add(Cart cart)
        {
            orders.Add(cart);
        }
    }
}