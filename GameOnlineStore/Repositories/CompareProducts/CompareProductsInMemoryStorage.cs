using GameOnlineStore.Models;
using OnlineShopWebApplication;

namespace GameOnlineStore.Repositories.CompareProducts
{
    public class CompareProductsInMemoryStorage : ICompareProducts
    {
        //public string UserId;
        public List<ProductsCollection> CompareProducts = new List<ProductsCollection>();

        public ProductsCollection TryGetByUserId(string userId)
        {
            return CompareProducts.FirstOrDefault(compare => compare.UserId == userId);
        }

        public void Add(Product product)
        {
            var compare = TryGetByUserId(Constants.UserId);
            if (compare == null)
            {
                compare = new ProductsCollection { UserId = Constants.UserId, Products = new List<Product>() };
                compare.Products.Add(product);
                CompareProducts.Add(compare);
            }
            else
            {
                var existingCompareProduct = compare.Products.FirstOrDefault(compareProduct => compareProduct.Id == product.Id);
                if (existingCompareProduct == null)
                {
                    compare.Products.Add(product);
                }
            }
        }

        public void Remove(Product product)
        {
            var compare = TryGetByUserId(Constants.UserId);
            if (compare == null)
            {
                return;
            }
            compare.Products.Remove(product);
        }
    }
}
