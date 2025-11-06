using GameOnlineStore.Db;
using GameOnlineStore.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOnlineStore.Repositories.FavoriteProducts
{
    public class FavoriteDbRepository : IFavoriteDbRepository
    {
        private readonly ApplicationContext context;

        public FavoriteDbRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(string userId, Product product)
        {
            var existingProduct = context.FavoriteProducts.FirstOrDefault(p => p.UserId == userId && p.Product.Id == product.Id);
            if (existingProduct == null)
            {
                context.FavoriteProducts.Add(new FavoriteProduct { Product = product, UserId = userId});
                context.SaveChanges();
            }
        }

        public void Remove(string userId, Guid productId)
        {
            var userFavoriteProduct = context.FavoriteProducts.FirstOrDefault(p => p.UserId == userId && p.Product.Id == productId);
            context.FavoriteProducts.Remove(userFavoriteProduct);
            context.SaveChanges();
        }

        public List<Product> GetAll(string userId) { 
            return context.FavoriteProducts
                .Where(p => p.UserId == userId)
                .Include(p => p.Product)
                .Select(p => p.Product)
                .ToList();
        }

        public void Clear(string userId)
        {
            var userFavoriteProducts = context.FavoriteProducts.Where(p => p.UserId == userId).ToList();
            context.FavoriteProducts.RemoveRange(userFavoriteProducts);
            context.SaveChanges();
        }
    }
}
