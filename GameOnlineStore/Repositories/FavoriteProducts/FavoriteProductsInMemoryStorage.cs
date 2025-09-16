using GameOnlineStore.Db.Models;
using OnlineShopWebApplication;

namespace GameOnlineStore.Repositories.FavoriteProducts
{
    public class FavoriteProductsInMemoryStorage : IFavoriteProducts
    {
        //public string UserId;
        public List<ProductsCollection> FavoriteProducts = new List<ProductsCollection>();

        public ProductsCollection TryGetByUserId(string userId)
        {
            return FavoriteProducts.FirstOrDefault(favorite => favorite.UserId == userId);
        }

        public void Add(Product product)
        {
            var favorite = TryGetByUserId(Constants.UserId);
            if (favorite == null)
            {
                favorite = new ProductsCollection { UserId = Constants.UserId, Products = new List<Product>() };
                favorite.Products.Add(product);
                FavoriteProducts.Add(favorite);
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
