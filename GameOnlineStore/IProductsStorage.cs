using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IProductsStorage
    {
        public List<Product> GetAll();
        public Product GetByProductId(int? productId);
    }
}
