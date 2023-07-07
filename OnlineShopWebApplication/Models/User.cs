namespace OnlineShopWebApplication.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string? Phone { get; set; }
        public Login Login { get; set; }
    }
}
