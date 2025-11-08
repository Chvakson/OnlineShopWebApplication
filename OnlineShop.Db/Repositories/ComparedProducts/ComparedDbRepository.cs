using GameOnlineStore.Db;
using GameOnlineStore.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOnlineStore.Repositories.ComparedProducts
{
    public class ComparedDbRepository : IComparedDbRepository
    {
        private readonly ApplicationContext context;

        public ComparedDbRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public void Add(string userId, Product product)
        {
            var existingProduct = context.ComparedProducts.FirstOrDefault(p => p.UserId == userId && p.Product.Id == product.Id);
            if (existingProduct == null)
            {
                context.ComparedProducts.Add(new ComparedProduct { Product = product, UserId = userId});
                context.SaveChanges();
            }
        }

        public void Remove(string userId, Guid productId)
        {
            var userComparedProduct = context.ComparedProducts.FirstOrDefault(p => p.UserId == userId && p.Product.Id == productId);
            context.ComparedProducts.Remove(userComparedProduct);
            context.SaveChanges();
        }

        public List<Product> GetAll(string userId) { 
            return context.ComparedProducts
                .Where(p => p.UserId == userId)
                .Include(p => p.Product)
                .Select(p => p.Product)
                .ToList();
        }

        public void Clear(string userId)
        {
            var userComparedProducts = context.ComparedProducts.Where(p => p.UserId == userId).ToList();
            context.ComparedProducts.RemoveRange(userComparedProducts);
            context.SaveChanges();
        }
    }
}
