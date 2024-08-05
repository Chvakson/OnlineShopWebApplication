using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Areas.Admin.Models
{
    public class ChangePassword
    {
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Login { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 4)]
        public string Password { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 4)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
}
