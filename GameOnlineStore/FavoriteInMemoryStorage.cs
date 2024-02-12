using GameOnlineStore.Models;

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

        public void Add(Product product, string userId)
        {
            var favorite = TryGetByUserId(userId);
            var existingFavoriteProduct = favorite.FavoriteProducts.FirstOrDefault(favoriteProduct => favoriteProduct.Id == product.Id);
            if (existingFavoriteProduct == null)
            {
                favorite.FavoriteProducts.Add(product);
            }
        }
    }
}
