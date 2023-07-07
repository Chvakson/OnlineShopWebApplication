using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IUsersStorage
    {
        User TryGetByUserId(Guid userId);
        public void Add(User user);
    }
}