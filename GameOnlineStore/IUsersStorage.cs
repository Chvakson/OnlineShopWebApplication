using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IUsersStorage
    {
        public List<Login> GetAll();
        //public Login GetUserById(Guid id);
        public Login Login(Login loginInfo);
        public void Register(Register registerInfo);
    }
}
