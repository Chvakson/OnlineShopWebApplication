using OnlineShopWebApplication;

namespace GameOnlineStore.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderDateTime;
        public List<CartItem> Items { get; set; }
        public UserDeliveryInfo UserDeliveryInfo { get; set; }
        public OrderStatus Status { get; set; }

        public Order()
        {
            Id = Guid.NewGuid();
            Status = OrderStatus.Created;
            OrderDateTime = DateTime.Now;
        }

        public decimal Cost
        {
            get
            {
                return Items?.Sum(item => item.Cost) ?? 0;
            }
        }
    }
}
