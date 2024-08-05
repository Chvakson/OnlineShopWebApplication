using GameOnlineStore.Areas.Admin.Models;

namespace GameOnlineStore.Models.User
{
    public class UserAccount
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public string Phone { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Role> Roles { get; set; }
    }
}
