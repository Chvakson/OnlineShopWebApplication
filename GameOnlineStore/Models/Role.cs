using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Models
{
    public class Role
    {
        [Required]
        public string Name { get; set; }
        public Role(){ }

        public Role(string name)
        {
            Name = name;
        }
    }
}
