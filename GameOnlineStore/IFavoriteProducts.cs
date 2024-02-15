using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IFavoriteProducts
    {
        public ProductsCollection TryGetByUserId(string userId);
        public void Add(Product product);
        public void Remove(Product product);
    }
}