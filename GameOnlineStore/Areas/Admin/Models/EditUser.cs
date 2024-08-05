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
        [RegularExpression(@"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$", ErrorMessage = "Указан неверный номер телефона.")]
        public string Phone { get; set; }
    }
}
