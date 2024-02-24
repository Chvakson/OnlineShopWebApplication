using OnlineShopWebApplication;

namespace GameOnlineStore.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<CartItem> Items { get; set; }
        public UserDeliveryInfo UserDeliveryInfo { get; set; }
        public Order()
        {
            Id = Guid.NewGuid();
        }

    }
}
