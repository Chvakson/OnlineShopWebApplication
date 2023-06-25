using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface ICartsStorage
    {
        public Cart TryGetByUserId(string userId);
        public void Add(Product product, string userId);
        public void Remove(Product product, string userId);
        void Clear(string userId);
    }
}
