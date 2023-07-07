using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public class UsersMemoryStorage : IUsersStorage
    {
        public List<User> users = new List<User>();

        public User TryGetByUserId(Guid userId)
        {
            return users.FirstOrDefault(data => data.UserId == userId);
        }

        public void Add(User user)
        {
            users.Add(user);
        }
    }
}
