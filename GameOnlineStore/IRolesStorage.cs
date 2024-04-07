using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public interface IRolesStorage
    {
        public void Add();
        public List<Role> GetAll();
    }
}
