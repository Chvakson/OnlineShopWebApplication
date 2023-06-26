using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IFavoriteStorage
    {
        public void Add(Product product);
    }
}