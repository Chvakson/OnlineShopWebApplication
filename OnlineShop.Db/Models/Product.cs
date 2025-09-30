using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public string Description { get; set; }

        public List<CartItem> CartItems { get; set; }  

        public string ImgFileName { get; set; }

        public string ImgPath
        {
            get
            {
                const string root = "/img/games/";
                return string.IsNullOrEmpty(ImgFileName)
                    ? $"{root}default.jpg"
                    : $"{root}{ImgFileName}";
            }
        }
    }
}
