using GameOnlineShop.Db.Models;

namespace GameOnlineShop.Repositories.FavoriteProducts
{
    public interface IFavoriteDbRepository
    {
        void Add(string userId, Product product);
        void Remove(string userId, Guid productId);
        List<Product> GetAll(string userId);
        void Clear(string userId);
    }
}