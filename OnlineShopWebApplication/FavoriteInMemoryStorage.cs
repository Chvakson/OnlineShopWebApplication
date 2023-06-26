using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public class FavoriteInMemoryStorage : IFavoriteStorage
    {
        public List<Product> Favorites = new List<Product>();

        public void Add(Product product) 
        {
            if(Favorites.Contains(product))
            {
                return;
            }
            Favorites.Add(product);
        }
    }
}
