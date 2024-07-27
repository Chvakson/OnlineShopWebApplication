using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Areas.Admin.Models
{
    public class EditUser
    {
        [Required]
        public string PrevLogin { get; set; }
        [Required]
        public string NewLogin { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
