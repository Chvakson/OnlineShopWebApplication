using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IRolesStorage
    {
        public List<Role> GetAll();
        public Role? TryGetByName(string name);
        public void Add(Role role);
        public void Remove(string name);
    }
}
