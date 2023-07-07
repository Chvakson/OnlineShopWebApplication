using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IUsersStorage
    {
        User TryGetByLogin(Login login);
        public void Add(User user);
    }
}