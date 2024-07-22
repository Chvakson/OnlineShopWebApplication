using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IUsersManager
    {
        void Add(UserAccount user);
        List<UserAccount> GetAll();
        UserAccount TryGetByName(string login);
        void ChangePassword(string login, string newPassword);
    }
}