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
        void Remove(string login);
    }
}