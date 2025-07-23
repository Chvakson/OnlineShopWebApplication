using GameOnlineStore.Areas.Admin.Models;
using System.Linq;

namespace GameOnlineStore.Repositories.Roles
{
    public class RolesInMemoryStorage : IRolesStorage
    {
        private readonly List<Role> roles = new()
        {
            new Role("Администратор"),
            new Role("Пользователь")
        };

        public List<Role> GetAll()
        {
            return roles;
        }

        public Role? TryGetByName(string name)
        {
            return roles.FirstOrDefault(role => role.Name == name);
        }

        public void Add(Role role)
        {
            roles.Add(new Role(role.Name));
        }

        public void Remove(string name)
        {
            var existingRole = TryGetByName(name);
            if (existingRole != null)
            {
                roles.Remove(existingRole);
            }
        }
    }
}
