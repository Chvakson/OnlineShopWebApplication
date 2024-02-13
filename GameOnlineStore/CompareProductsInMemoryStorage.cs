using GameOnlineStore.Models;
using OnlineShopWebApplication;

namespace GameOnlineStore
{
    public class CompareProductsInMemoryStorage : IProductsCollection
    {
        //public string UserId;
        public List<ProductsCollection> CompareProducts = new List<ProductsCollection>();

        public ProductsCollection TryGetByUserId(string userId)
        {
            return CompareProducts.FirstOrDefault(favorite => favorite.UserId == userId);
        }

        public void Add(Product product)
        {
            var favorite = TryGetByUserId(Constants.UserId);
            if (favorite == null)
            {
                favorite = new ProductsCollection { UserId = Constants.UserId, Products = new List<Product>() };
                favorite.Products.Add(product);
                CompareProducts.Add(favorite);
            }
            else
            {
                var existingFavoriteProduct = favorite.Products.FirstOrDefault(favoriteProduct => favoriteProduct.Id == product.Id);
                if (existingFavoriteProduct == null)
                {
                    favorite.Products.Add(product);
                }
            }
        }

        public void Remove(Product product)
        {
            var favorite = TryGetByUserId(Constants.UserId);
            if (favorite == null)
            {
                return;
            }
            favorite.Products.Remove(product);
        }
    }
}
