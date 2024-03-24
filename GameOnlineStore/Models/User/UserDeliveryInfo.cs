using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Models
{
    public class UserDeliveryInfo
    {
        [Required(ErrorMessage = "Не указано имя")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Не указан email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Не указан номер телефона")]
        public string Phone { get; set; }
        public UserAddress UserAddress { get; set; }
        public string? Comment { get; set; }
    }
}
