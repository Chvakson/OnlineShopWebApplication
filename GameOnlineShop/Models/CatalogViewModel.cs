namespace GameOnlineShop.Models
{
    public class CatalogViewModel
    {
        public List<ProductViewModel> Products { get; set; } = new();
        public List<string> AvailableGenres { get; set; } = new();
        public List<string> SelectedGenres { get; set; } = new();
        public string? Query { get; set; }
        public string? Sort { get; set; }
        public ProductViewModel? Featured { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int TotalCount { get; set; }
        public int[] PageSizeOptions { get; } = { 10, 20, 50 };

        public string? GenresParam => Helpers.GenreFilter.Join(SelectedGenres);

        public int TotalPages => PageSize <= 0 || TotalCount <= 0
            ? 1
            : (int)Math.Ceiling(TotalCount / (double)PageSize);

        public int FromItem => TotalCount == 0 ? 0 : (Page - 1) * PageSize + 1;
        public int ToItem => Math.Min(Page * PageSize, TotalCount);

        public bool IsGenreSelected(string tag) => Helpers.GenreFilter.IsSelected(SelectedGenres, tag);

        public string? ToggleGenres(string tag) => Helpers.GenreFilter.Toggle(GenresParam, tag);
    }
}
