using GameOnlineStore.Db.Models;
namespace GameOnlineStore.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public UserDeliveryInfoViewModel UserDeliveryInfo { get; set; }
        public OrderStatusViewModel Status { get; set; }
        public DateTime CreatedDateTime { get; set; }

        public decimal Cost
        {
            get
            {
                return Items?.Sum(item => item.Cost) ?? 0;
            }
        }
    }
}
