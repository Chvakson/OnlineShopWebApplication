namespace GameOnlineStore.Models
{
    public class UserContacts
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public UserAddress Address { get; set; }
    }
}
