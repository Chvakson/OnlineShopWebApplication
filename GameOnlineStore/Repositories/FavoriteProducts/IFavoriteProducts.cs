using GameOnlineStore.Db.Models;

namespace GameOnlineStore.Repositories.FavoriteProducts
{
    public interface IFavoriteProducts
    {
        public ProductsCollection TryGetByUserId(string userId);
        public void Add(Product product);
        public void Remove(Product product);
    }
}