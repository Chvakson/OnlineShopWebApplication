using GameOnlineStore.Db.Models;
using Microsoft.EntityFrameworkCore;

namespace GameOnlineStore.Db.Repositories.Carts
{
    public class CartsDbRepository : ICartsDbRepository
    {
        private readonly ApplicationContext context;

        public CartsDbRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public Cart TryGetByUserId(string userId)
        {
            return context.Carts
                .Include(c => c.Items)
                .ThenInclude(c => c.Product)
                .FirstOrDefault(cart => cart.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart == null)
            {
                var cart = new Cart()
                {
                    UserId = userId,
                };

                cart.Items = new List<CartItem>
                {
                    new CartItem
                    {
                        Product = product,
                        Amount = 1,
                    }
                };

                context.Carts.Add(cart);
            }
            else
            {
                var existingCartItem = existingCart.Items.FirstOrDefault(cartItem => cartItem.Product.Id == product.Id);
                if (existingCartItem == null)
                {
                    existingCart.Items.Add(new CartItem
                    {
                        Product = product,
                        Amount = 1,
                    });
                }
                else
                    existingCartItem.Amount++;
            }
            context.SaveChanges();
        }

        public void Remove(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            var existingCartItem = existingCart.Items.FirstOrDefault(cartItem => cartItem.Product.Id == product.Id);
            if (existingCartItem == null)
                return;

            existingCartItem.Amount--;

            if (existingCartItem.Amount == 0)
                existingCart.Items.Remove(existingCartItem);

            context.SaveChanges();
        }

        public void Clear(string userId)
        {
            var existingCart = TryGetByUserId(userId);
            context.Carts.Remove(existingCart);
            context.SaveChanges();
        }
    }
}
