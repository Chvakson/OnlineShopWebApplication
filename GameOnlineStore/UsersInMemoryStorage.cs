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

        public bool IsUserCredentialsValid(LoginCredential loginInfo)
        {
            var existingUser = UsersLogins.FirstOrDefault(existingUser => existingUser.Login == loginInfo.Login && existingUser.Password == loginInfo.Password);
            if (existingUser == null)
            {
                return false;
            }
            return true;
        }

        public void RegisterNewUser(RegisterDetails registerInfo)
        {
            UsersLogins.Add(new LoginCredential
            {
                Login = registerInfo.Login,
                Password = registerInfo.Password
            });
        }
    }
}
