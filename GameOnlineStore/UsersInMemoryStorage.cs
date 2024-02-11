using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public class UsersInMemoryStorage : IUsersStorage
    {
        public List<User> Users = new List<User>();
    }
}
