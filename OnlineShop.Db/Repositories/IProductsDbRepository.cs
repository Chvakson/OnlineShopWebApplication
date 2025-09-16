using GameOnlineStore.Db.Models;

namespace GameOnlineStore.Db.Repositories
{
    public interface IProductsDbRepository
    {
        public List<Product> GetAll();
        public Product TryGetById(Guid? productId);
        public void Add(Product product);
        public void Remove(Guid productId);
        public void Update(Product product);
    }
}
