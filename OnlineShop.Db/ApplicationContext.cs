using GameOnlineStore.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOnlineStore.Db
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
    }
}
