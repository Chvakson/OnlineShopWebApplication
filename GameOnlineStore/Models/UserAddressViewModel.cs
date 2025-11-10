using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Models
{
    public class UserAddressViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Не указан город")]
        [StringLength(75)]
        public string City { get; set; }
        [Required(ErrorMessage = "Не указана улица")]
        [StringLength(75)]
        public string Street { get; set; }
        public string? Entrance { get; set; }
        [Required(ErrorMessage = "Не указан номер дома")]
        [StringLength(15)]
        public string HomeNo { get; set; }
        [StringLength(15)]
        public string? FloorNo { get; set; }
        [StringLength(15)]
        public string? FlatNo { get; set; }
    }
}
