using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public class UsersInMemoryStorage : IUsersStorage
    {
        public List<LoginCredential> UsersLogins = new List<LoginCredential>();

        public List<LoginCredential> GetAll()
        {
            return UsersLogins;
        }

        //public Login GetUserById(Guid id)
        //{
        //    return UsersLogins.FirstOrDefault(user => user.Id == id);
        //}

        public LoginCredential Login(LoginCredential loginInfo)
        {
            return UsersLogins.FirstOrDefault(existingUser => existingUser.Login == loginInfo.Login && existingUser.Password == loginInfo.Password);
        }

        public void Register(RegisterDetails registerInfo)
        {
            UsersLogins.Add(new LoginCredential
            {
                Login = registerInfo.Login,
                Password = registerInfo.Password
            });
        }
    }
}
