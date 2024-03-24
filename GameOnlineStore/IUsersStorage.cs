using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IUsersStorage
    {
        public List<LoginCredential> GetAll();
        //public Login GetUserById(Guid id);
        public LoginCredential Login(LoginCredential loginInfo);
        public void RegisterNewUser(RegisterDetails registerInfo);
    }
}
