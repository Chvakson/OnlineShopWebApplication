using OnlineShopWebApplication;

namespace GameOnlineStore.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public Cart Cart { get; set; }
        public UserContacts UserContacts { get; set; }
        public UserAddress UserAddress { get; set; }
        public string? Comment { get; set; }
    }
}
