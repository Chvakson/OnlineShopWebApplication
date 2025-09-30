using GameOnlineStore.Db.Models;
using GameOnlineStore.Db.Repositories.Products;
using GameOnlineStore.Models;

namespace GameOnlineStore.Helpers
{
    public class Mapping
    {
        public static ProductViewModel ToProductViewModel(Product productDb)
        {
            return new ProductViewModel()
            {
                Id = productDb.Id,
                Name = productDb.Name,
                Cost = productDb.Cost,
                Description = productDb.Description,
                ImgFileName = productDb.ImgFileName,
            };
        }

        public static List<ProductViewModel> ToProductViewModels(List<Product> productsDb)
        {
            var productViewModels = new List<ProductViewModel>();
            foreach (var product in productsDb) {
                productViewModels.Add(ToProductViewModel(product));
            }
            return productViewModels;
        }

        public static List<CartItemViewModel> ToCartItemViewModels(List<CartItem> cartDbItems)
        {
            List<CartItemViewModel> cartItemViewModels = new();
            foreach (var item in cartDbItems)
            {
                var carItemViewModel = new CartItemViewModel()
                {
                    Id = item.Id,
                    Amount = item.Amount,
                    Product = ToProductViewModel(item.Product)
                };
                cartItemViewModels.Add(carItemViewModel);
            }
            return cartItemViewModels;
        }

        public static CartViewModel ToCartViewModel(Cart cartDb)
        {
            if (cartDb == null)
                return null;

            return new CartViewModel{
                Id = cartDb.Id,
                UserId = cartDb.UserId,
                Items = ToCartItemViewModels(cartDb.Items)
            };
        }
    }
}
