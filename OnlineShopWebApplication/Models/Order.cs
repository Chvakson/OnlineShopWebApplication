namespace OnlineShopWebApplication.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public User user { get; set; }
        public List<CartItem> items { get; set; }
        public DateTime Date { get; set; }
        public Enum Statuses { get; set; }

        public decimal Cost
        {
            get
            {
                decimal value = 0;
                foreach (var item in items)
                {
                    value += item.Cost;
                }
                return value;
            }
        }
    }
}
