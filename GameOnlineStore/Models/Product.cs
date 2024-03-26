using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Models
{
    public class Product
    {
        private static int instanceCounter = 1;
        public int Id { get; set; }
        [Required(ErrorMessage = "Не указано название продукта")]
        [StringLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана стоимость продукта")]
        public int Cost { get; set; }
        [Required(ErrorMessage = "Не указано описание продукта")]
        public string Description { get; set; }

        private string? _imgPath;

        public string? ImgPath
        {
            get { return _imgPath ?? "default.jpg"; }
            set { _imgPath = value; }
        }


        public Product(string name, int cost, string description, string? imgPath)
        {
            Id = instanceCounter++;
            Name = name;
            Cost = cost;
            Description = description;
            ImgPath = imgPath ?? "default.jpg";
        }

        public Product()
        {

        }
    }
}
