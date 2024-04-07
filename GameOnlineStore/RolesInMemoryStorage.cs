using GameOnlineStore.Models;

namespace GameOnlineStore
{
    public class RolesInMemoryStorage : IRolesStorage
    {
        public List<Role> Roles = new();

        public void Add()
        {
            Roles.Add(new Role("Admin"));
        }

        public List<Role> GetAll()
        {
            return Roles;
        }

        //public void Remove()
        //{
        //    Roles.Remove(new Role());
        //}
    }
}
