using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public class UsersInMemoryStorage : IUsersStorage
    {
        public List<Login> UsersLogins = new List<Login>();

        public List<Login> GetAll()
        {
            return UsersLogins;
        }

        //public Login GetUserById(Guid id)
        //{
        //    return UsersLogins.FirstOrDefault(user => user.Id == id);
        //}

        public Login Login(Login loginInfo)
        {
            return UsersLogins.FirstOrDefault(existingUser => existingUser.UserName == loginInfo.UserName && existingUser.Password == loginInfo.Password);
        }

        public void Register(Register registerInfo)
        {
            UsersLogins.Add(new Login
            {
                UserName = registerInfo.UserName,
                Password = registerInfo.Password
            });
        }
    }
}
