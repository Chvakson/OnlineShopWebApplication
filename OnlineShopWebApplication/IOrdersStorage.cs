using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IOrdersStorage
    {
        public void Add(Cart cart);
        public void Add(User userData);
    }
}
