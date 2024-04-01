using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IOrdersStorage
    {
        public List<Order> GetAll();
        public Order? TryGetById(Guid id);
        public void Add(Order order);
    }
}
