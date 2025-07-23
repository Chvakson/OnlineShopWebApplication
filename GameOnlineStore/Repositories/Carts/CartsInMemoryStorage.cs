using GameOnlineStore.Models;

namespace GameOnlineStore.Repositories.Carts
{
    public class CartsInMemoryStorage : ICartsStorage
    {
        private List<Cart> carts = new List<Cart>();

        public Cart TryGetByUserId(string userId)
        {
            return carts.FirstOrDefault(cart => cart.UserId == userId);
        }

        public void Add(Product product, string userId)
        {
            var existingCart = TryGetByUserId(userId);
            if (existingCart == null)
            {
                var cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            Id = Guid.NewGuid(),
                            Product = product,
                            Amount = 1
                        }
                    }
                };
                carts.Add(cart);
            }
            else
            {
                var existingCartItem = existingCart.Items.FirstOrDefault(cartItem => cartItem.Product.Id == product.Id);
                if (existingCartItem == null)
                {
                    existingCart.Items.Add(new CartItem
                    {
                        Id = Guid.NewGuid(),
                        Product = product,
                        Amount = 1
                    });
                }
                else
                    existingCartItem.Amount++;
            }
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
        }

        public void Clear(string userId)
        {
            var existingCart = TryGetByUserId(userId);
            carts.Remove(existingCart);
        }
    }
}
