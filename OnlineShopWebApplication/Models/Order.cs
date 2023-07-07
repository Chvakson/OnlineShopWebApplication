namespace OnlineShopWebApplication.Models
{
    public class Order
    {
        public Guid userId { get; set; }
        public Cart cart { get; set; }
    }
}
