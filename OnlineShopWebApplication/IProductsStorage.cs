using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IProductsStorage
    {
        public List<Product> GetAll();
        public Product TryGetById(int? id);
        public void RemoveById(int id);
        public void Add(Product product);
    }
}
