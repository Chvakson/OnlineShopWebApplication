using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public class RolesInMemoryStorage : IRolesStorage
    {
        private readonly List<Role> roles = new List<Role>();
        public List<Role> GetAll()
        {
            return roles;   
        }
        public Role TryGetById(string name)
        {
            return roles.FirstOrDefault(role => role.Name == name);
        }
        public void Add(Role role)
        {
            roles.Add(role);
        }
        public void Remove(string name)
        {
            roles.RemoveAll(role => role.Name == name);
        }
    }
}
