using OnlineShopWebApplication.Models;

namespace OnlineShopWebApplication
{
    public class CartsStorage : ICartsStorage
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
                var newCart = new Cart
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
                carts.Add(newCart);
            }
            else
            {
                var existingCartItem = existingCart.Items.FirstOrDefault(item => item.Product.Id == product.Id);
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
                {
                    existingCartItem.Amount++;
                }
            }
        }
    }
}
