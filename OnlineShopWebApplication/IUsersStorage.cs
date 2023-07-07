using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IUsersStorage
    {
        User TryGetByLogin(Login login);
        public List<User> GetAll();
        public void Add(User user);
    }
}