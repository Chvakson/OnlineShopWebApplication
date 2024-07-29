using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Areas.Admin.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; }
        public Role() { }

        public Role(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
