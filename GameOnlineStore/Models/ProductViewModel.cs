using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Models
{
    public class ProductViewModel
    {
        private const string imageRootPath = "/img/games/";

        public Guid Id { get; set; }
        [Required(ErrorMessage = "Не указано название продукта")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана стоимость продукта")]
        public int Cost { get; set; }
        [Required(ErrorMessage = "Не указано описание продукта")]
        public string Description { get; set; }
        public string? ImgFileName { get; set; }

        public string ImgPath
        {
            get
            {
                return string.IsNullOrEmpty(ImgFileName)
                    ? $"{imageRootPath}default.jpg"
                    : $"{imageRootPath}{ImgFileName}";
            }
        }
    }
}
