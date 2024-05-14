using GameOnlineStore.Areas.Admin.Models;

namespace GameOnlineStore.Models.User
{
    public class User
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }

        public User(string login, string password, string firstName, string lastName, string phone)
        {
            Id = new Guid();
            Role = new Role("User");
            Login = login;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
        }
    }
}
