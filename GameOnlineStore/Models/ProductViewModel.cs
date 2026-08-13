using System.ComponentModel.DataAnnotations;

namespace GameOnlineStore.Models
{
    public class ProductViewModel
    {
        private const string imageRootPath = "/img/games/";

        public Guid Id { get; set; }
        [Required(ErrorMessage = "Не указано название продукта")]
        [StringLength(80)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указана стоимость продукта")]
        public int Cost { get; set; }
        [Required(ErrorMessage = "Не указано описание продукта")]
        public string Description { get; set; }
        public string? ImgFileName { get; set; }
        public string? Genre { get; set; }
        public string? Developer { get; set; }
        public int? ReleaseYear { get; set; }

        public string? ImgPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImgFileName))
                {
                    return $"{imageRootPath}default.jpg";
                }

                if (ImgFileName.StartsWith("http", StringComparison.OrdinalIgnoreCase) ||
                    ImgFileName.StartsWith('/'))
                {
                    return ImgFileName;
                }

                return $"{imageRootPath}{ImgFileName}";
            }
        }

        public IReadOnlyList<string> GenreTags
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Genre))
                {
                    return Array.Empty<string>();
                }

                return Genre
                    .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                    .Where(tag => !string.IsNullOrWhiteSpace(tag))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }
        }
    }
}
