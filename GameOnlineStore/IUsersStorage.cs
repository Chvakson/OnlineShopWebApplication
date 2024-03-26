using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IUsersStorage
    {
        public List<LoginCredential> GetAll();
        //public Login GetUserById(Guid id);
        public bool IsUserCredentialsValid(LoginCredential loginInfo);
        public void RegisterNewUser(RegisterDetails registerInfo);
    }
}
