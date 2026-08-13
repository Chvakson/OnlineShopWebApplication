using GameOnlineShop.Db.Repositories.Products;
using GameOnlineShop.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace GameOnlineShop.Models.Controllers
{
    public class HomeController : Controller
    {
        private static readonly int[] AllowedPageSizes = { 10, 20, 50 };
        private const int DefaultPageSize = 20;

        private readonly IProductsDbRepository productsRepository;

        public HomeController(IProductsDbRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public IActionResult Index(string? query, string? genre, string? genres, string? sort, int? login, int page = 1, int pageSize = DefaultPageSize)
        {
            TempData.Keep("ReturnUrl");
            var showFromTemp = TempData["ShowLoginModal"];

            if (login == 1 ||
                showFromTemp is true ||
                string.Equals(showFromTemp?.ToString(), "True", StringComparison.OrdinalIgnoreCase))
            {
                ViewBag.ShowModal = true;
            }

            pageSize = AllowedPageSizes.Contains(pageSize) ? pageSize : DefaultPageSize;
            var selectedGenres = GenreFilter.Parse(genres, genre);

            var allProducts = productsRepository.GetAll().ToProductViewModels();
            var availableGenres = allProducts
                .SelectMany(product => product.GenreTags)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(tag => tag)
                .ToList();

            var products = allProducts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                products = products.Where(product =>
                    ContainsIgnoreCase(product.Name, query) ||
                    ContainsIgnoreCase(product.Genre, query) ||
                    ContainsIgnoreCase(product.Developer, query) ||
                    ContainsIgnoreCase(product.Description, query));
            }

            if (selectedGenres.Count > 0)
            {
                products = products.Where(product =>
                    product.GenreTags.Any(tag => selectedGenres.Contains(tag, StringComparer.OrdinalIgnoreCase)));
            }

            products = sort switch
            {
                "price_asc" => products.OrderBy(product => product.Cost),
                "price_desc" => products.OrderByDescending(product => product.Cost),
                "name" => products.OrderBy(product => product.Name),
                "year" => products.OrderByDescending(product => product.ReleaseYear ?? 0),
                _ => products.OrderBy(product => product.Name)
            };

            var filtered = products.ToList();
            var totalCount = filtered.Count;
            var totalPages = Math.Max(1, (int)Math.Ceiling(totalCount / (double)pageSize));
            page = Math.Clamp(page < 1 ? 1 : page, 1, totalPages);

            var pageItems = filtered
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return View(new CatalogViewModel
            {
                Products = pageItems,
                AvailableGenres = availableGenres,
                Query = query,
                SelectedGenres = selectedGenres,
                Sort = sort,
                Page = page,
                PageSize = pageSize,
                TotalCount = totalCount,
                Featured = allProducts.OrderByDescending(product => product.Cost).FirstOrDefault()
            });
        }

        [HttpGet]
        public IActionResult Suggest(string? q)
        {
            var term = q?.Trim() ?? string.Empty;
            if (term.Length < 1)
            {
                return Json(Array.Empty<object>());
            }

            var products = productsRepository.GetAll();
            var suggestions = new List<(string Text, string Kind, int Rank)>();

            foreach (var product in products)
            {
                AddSuggestion(suggestions, product.Name, "Игра", term, 0);
                AddSuggestion(suggestions, product.Developer, "Студия", term, 2);

                if (string.IsNullOrWhiteSpace(product.Genre))
                {
                    continue;
                }

                foreach (var tag in product.Genre.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
                {
                    AddSuggestion(suggestions, tag, "Жанр", term, 1);
                }
            }

            var result = suggestions
                .GroupBy(item => item.Text, StringComparer.OrdinalIgnoreCase)
                .Select(group => group.OrderBy(item => item.Rank).First())
                .OrderBy(item => item.Rank)
                .ThenBy(item => item.Text.StartsWith(term, StringComparison.OrdinalIgnoreCase) ? 0 : 1)
                .ThenBy(item => item.Text)
                .Take(8)
                .Select(item => new { text = item.Text, kind = item.Kind })
                .ToList();

            return Json(result);
        }

        private static void AddSuggestion(List<(string Text, string Kind, int Rank)> suggestions, string? value, string kind, string term, int rank)
        {
            if (ContainsIgnoreCase(value, term))
            {
                suggestions.Add((value!.Trim(), kind, rank));
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        private static bool ContainsIgnoreCase(string? source, string query)
        {
            return !string.IsNullOrWhiteSpace(source) &&
                   source.Contains(query.Trim(), StringComparison.OrdinalIgnoreCase);
        }
    }
}
