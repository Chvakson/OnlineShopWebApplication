namespace GameOnlineStore.Models
{
    public class UserDeliveryInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserAddress UserAddress { get; set; }
        public string? Comment { get; set; }
    }
}
