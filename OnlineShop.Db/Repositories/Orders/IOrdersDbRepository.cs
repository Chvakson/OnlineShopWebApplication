using GameOnlineStore.Db.Models;

namespace GameOnlineStore.Db.Repositories.Orders
{
    public interface IOrdersDbRepository
    {
        public List<Order> GetAll();
        public Order? TryGetById(Guid id);
        public void Add(Order order);
        public void UpdateStatus(Guid orderId, int newStatusId);
    }
}
