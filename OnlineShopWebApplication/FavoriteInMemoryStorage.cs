using Microsoft.AspNetCore.Mvc;
using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public class FavoriteInMemoryStorage : IFavoriteStorage
    {
        //public string UserId;
        public List<Product> Products = new List<Product>();

        public List<Product> GetAll()
        {
            return Products;
        }
        public void Add(Product product)
        {
            if (Products.Contains(product))
            {
                return;
            }
            Products.Add(product);
        }
    }
}
