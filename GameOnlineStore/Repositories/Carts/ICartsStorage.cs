using GameOnlineStore.Models;

namespace GameOnlineStore.Repositories.Carts
{
    public interface ICartsStorage
    {
        public Cart TryGetByUserId(string userId);
        public void Add(ProductViewModel product, string userId);
        public void Remove(ProductViewModel product, string userId);
        public void Clear(string userId);
    }
}