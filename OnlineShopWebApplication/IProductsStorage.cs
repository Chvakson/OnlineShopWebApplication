using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IProductsStorage
    {
        public List<Product> GetAll();
        public Product TryGetById(int? id);
    }
}
