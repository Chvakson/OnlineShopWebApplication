using GameOnlineStore.Areas.Admin.Models;
using GameOnlineStore.Models.User;

namespace GameOnlineStore
{
    public interface IUsersManager
    {
        void Add(UserAccount user);
        List<UserAccount> GetAll();
        UserAccount TryGetByName(string login);
        void ChangePassword(string login, string newPassword);
        void Edit(string prevLogin, string newLogin, string phone);
        public Permissions GetPermissions(string login);
        public void GivePermissions(string login, List<string> roleNames);
        void Remove(string login);
    }
}