namespace OnlineShopWebApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User user { get; set; }
        public List<CartItem> items { get; set; }
    }
}
