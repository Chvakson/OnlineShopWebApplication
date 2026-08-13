namespace GameOnlineStore.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public string Description { get; set; }

        public string? Genre { get; set; }

        public string? Developer { get; set; }

        public int? ReleaseYear { get; set; }

        public List<CartItem> CartItems { get; set; }  

        public string? ImgFileName { get; set; }

        public string? ImgPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImgFileName))
                {
                    return "/img/games/default.jpg";
                }

                if (ImgFileName.StartsWith("http", StringComparison.OrdinalIgnoreCase) ||
                    ImgFileName.StartsWith('/'))
                {
                    return ImgFileName;
                }

                return $"/img/games/{ImgFileName}";
            }
        }

        public Product()
        {
            CartItems = new List<CartItem>();
        }
    }
}
