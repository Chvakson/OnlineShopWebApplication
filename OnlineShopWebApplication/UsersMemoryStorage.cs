using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public class UsersMemoryStorage : IUsersStorage
    {
        public List<User> users = new List<User>();

        public User TryGetByLogin(Login login)
        {
            return users.FirstOrDefault(user => user.Login.Email == login.Email && user.Login.Password == login.Password);
        }

        public void Add(User user)
        {
            users.Add(user);
        }
    }
}
