using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public class UsersInMemoryStorage : IUsersStorage
    {
        public List<User> Users = new List<User>();

        public User GetUserById(Guid id)
        {
            return Users.FirstOrDefault(user => user.Id == id);
        }

        public User Login (User user)
        {
            return Users.FirstOrDefault(existingUser => existingUser.Login == user.Login && existingUser.Password == user.Password);
        }

        public void Register(User user)
        {
            Users.Add(
                new User
                {
                    Id = new Guid(),
                    Login = user.Login,
                    Password = user.Password
                }
                );
        }
    }
}
