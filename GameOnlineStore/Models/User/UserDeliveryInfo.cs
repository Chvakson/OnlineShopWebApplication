using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Models
{
    public class UserDeliveryInfo
    {
        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Не указан email")]
        [StringLength(75)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Указан неверный адрес электронной почты.")]
        public string Email { get; set; }
        //[Required(ErrorMessage = "Не указан номер телефона")]
        //[RegularExpression(@"^\+7\(\d{3}\)\d{3}-\d{2}-\d{2}$", ErrorMessage = "Указан неверный номер телефона.")] 
        public string Phone { get; set; }
        public UserAddress UserAddress { get; set; }
        [StringLength(250)]
        public string? Comment { get; set; }
    }
}
