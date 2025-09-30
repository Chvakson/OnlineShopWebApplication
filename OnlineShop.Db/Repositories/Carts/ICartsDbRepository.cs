using GameOnlineStore.Db.Models;

namespace GameOnlineStore.Db.Repositories.Carts
{
    public interface ICartsDbRepository
    {
        public Cart TryGetByUserId(string userId);
        public void Add(Product product, string userId);
        public void Remove(Product product, string userId);
        public void Clear(string userId);
    }
}