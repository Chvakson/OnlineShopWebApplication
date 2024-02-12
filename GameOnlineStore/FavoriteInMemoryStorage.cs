using GameOnlineStore.Models;
using OnlineShopWebApplication;

namespace GameOnlineStore
{
    public class FavoriteInMemoryStorage : IFavoriteStorage
    {
        //public string UserId;
        public List<Favorite> favoriteProducts = new List<Favorite>();

        public Favorite TryGetByUserId(string userId)
        {
            return favoriteProducts.FirstOrDefault(favorite => favorite.UserId == userId);
        }

        public void Add(Product product)
        {
            var favorite = TryGetByUserId(Constants.UserId);
            if (favorite == null)
            {
                favorite = new Favorite { UserId = Constants.UserId, FavoriteProducts = new List<Product>() };
                favorite.FavoriteProducts.Add(product);
                favoriteProducts.Add(favorite);
            }
            else
            {
                var existingFavoriteProduct = favorite.FavoriteProducts.FirstOrDefault(favoriteProduct => favoriteProduct.Id == product.Id);
                if (existingFavoriteProduct == null)
                {
                    favorite.FavoriteProducts.Add(product);
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
            favorite.FavoriteProducts.Remove(product);
        }
    }
}
