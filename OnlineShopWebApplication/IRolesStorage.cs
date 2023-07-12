using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public interface IRolesStorage
    {
        public List<Role> GetAll();
        public Role TryGetById(string name);
        public void Add(Role role);
        public void Remove(string name);
    }
}
