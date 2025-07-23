using GameOnlineStore.Models;
using System.Net.NetworkInformation;

namespace GameOnlineStore.Repositories.Orders
{
    public interface IOrdersStorage
    {
        public List<Order> GetAll();
        public Order? TryGetById(Guid id);
        public void Add(Order order);
        public void UpdateStatus(Guid id, OrderStatus status);
    }
}
