using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IUsersStorage
    {
        public List<User> GetAll();
        public User GetUserById(Guid id);
        public User Login(User user);
        public void Register(User user);
    }
}
