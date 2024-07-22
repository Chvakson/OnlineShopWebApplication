using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public class UsersManager : IUsersManager
    {
        private List<UserAccount> users = new List<UserAccount>();

        public List<UserAccount> GetAll()
        {
            return users;
        }

        public void Add(UserAccount user)
        {
            users.Add(user);
        }

        public UserAccount TryGetByName(string login)
        {
            return users.FirstOrDefault(user => user.Login == login);
        }

        public void ChangePassword(string login, string newPassword)
        {
            var account = TryGetByName(login);
            account.Password = newPassword;
        }
    }
}
