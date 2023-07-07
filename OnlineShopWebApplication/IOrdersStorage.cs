using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IOrdersStorage
    {
        public void Add(Order order);
    }
}
