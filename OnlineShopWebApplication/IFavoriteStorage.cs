using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IFavoriteStorage
    {
        public List<Product> GetAll();
        public void Add(Product product);
    }
}