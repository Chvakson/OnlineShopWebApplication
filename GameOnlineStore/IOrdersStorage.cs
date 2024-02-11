using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IOrdersStorage
    {
        public List<Order> TryGetByUserId(string userId);
        public void Add(Cart cart, string userId);
    }
}
