using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Models
{
    public class UserAddress
    {
        [Required(ErrorMessage ="Не указан город")]
        [StringLength(75, MinimumLength = 2, ErrorMessage ="Название города должно содержать от 2 до 75 символов")]
        public string City { get; set; }
        [Required(ErrorMessage = "Не указана улица")]
        [StringLength(75, MinimumLength = 2, ErrorMessage = "Название улицы должно содержать от 2 до 75 символов")]
        public string Street { get; set; }
        public string? Entrance { get; set; }
        [Required(ErrorMessage = "Не указан номер дома")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "Номер дома должен содержать от 2 до 15 символов")]
        public string HomeNo { get; set; }
        public string? FloorNo { get; set; }
        public string? FlatNo { get; set; }

        //public string City { get; set; }
        //public string Street { get; set; }
        //public string? Entrance { get; set; }
        //public string HomeNo { get; set; }
        //public string? FloorNo { get; set; }
        //public string? FlatNo { get; set; }
    }
}
