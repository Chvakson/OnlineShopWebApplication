namespace GameOnlineStore.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<CartItem> Items { get; set; }
        public UserDeliveryInfo UserDeliveryInfo { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public Order()
        {
            Status = OrderStatus.Created;
        }
    }
}
