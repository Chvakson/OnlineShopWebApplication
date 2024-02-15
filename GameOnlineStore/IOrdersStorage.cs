using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IOrdersStorage
    {
        public List<Order> GetAll();
        public List<Order> TryGetByUserId(string userId);
        public void Add(Cart cart, UserAddress userAddress, UserContacts userContacts, string? comment);
    }
}
