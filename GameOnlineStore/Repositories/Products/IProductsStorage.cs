using GameOnlineStore.Models;

namespace GameOnlineStore.Repositories.Products
{
    public interface IProductsStorage
    {
        public List<Product> GetAll();
        public Product TryGetById(int? productId);
        public void Add(Product product);
        public void Remove(int productId);
        public void Update(Product product);
    }
}
