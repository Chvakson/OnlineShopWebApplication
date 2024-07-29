using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Areas.Admin.Models
{
    public class Permissions
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Login { get; set; }
        public List<Role> Roles { get; set; }
    }
}
