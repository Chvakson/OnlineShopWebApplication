using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IFavoriteStorage
    {
        public Favorite TryGetByUserId(string userId);
        public void Add(Product product, string userId);
    }
}