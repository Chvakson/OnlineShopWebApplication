using GameOnlineStore.Areas.Admin.Models;
using GameOnlineStore.Models.User;
using System.Numerics;

namespace GameOnlineStore
{
    public class UsersManager : IUsersManager
    {
        private List<UserAccount> users = new();

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
        
        public void Edit(string prevLogin, string newLogin, string phone)
        {
            var account = TryGetByName(prevLogin);
            account.Login = newLogin;
            account.Phone = phone;
        }
        
        public void Remove(string login)
        {
            var account = TryGetByName(login);
            users.Remove(account);
        }

        public void GetPermissions(string login, List<Role> roles)
        {
            var account = TryGetByName(login);
            account.Roles = roles;
        }
    }
}
