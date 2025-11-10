using GameOnlineStore.Areas.Admin.Models;

namespace GameOnlineStore.Models
{
    public class UserAccount
    {
        Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Phone { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Role> Roles { get; set; }
    }
}
